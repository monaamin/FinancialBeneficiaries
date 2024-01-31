using AutoMapper;
using FinancialManagementDataLayer.Entities;
using FinancialManagementDataLayer.Repositories;
using FinancialManagementDataLayer.Repositories.Abstractions;
using FinancialManagementServices.ExternalServices;
using FinancialManagementServices.Models;
using Microsoft.Extensions.Logging;

namespace FinancialManagementServices.UserBeneficialServices
{
    public class TransactionManagementService : ITransactionManagementService
    {
        private readonly IUserRepository _userRepository;

        private readonly ITopUpTransactionRepository _topUpTransactionRepository;
        private readonly ITopUpOptionsRepository _topUpOptionsRepository;
        private readonly ILogger<TransactionManagementService> _logger;
        private readonly IMapper _mapper;
        private readonly IUserBalanceInformationService _userBalanceInformationService;
        private readonly ITopUpLimitOptionsRepository _topUpLimitOptionsRepository;
        CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public TransactionManagementService(ITopUpTransactionRepository topUpTransactionRepository, 
            ILogger<TransactionManagementService> logger, 
            IMapper mapper,
            IUserRepository userRepository,
            ITopUpOptionsRepository topUpOptionsRepository,
            IUserBalanceInformationService userBalanceInformationService,
            ITopUpLimitOptionsRepository topUpLimitOptionsRepository)
        {
            _topUpTransactionRepository = topUpTransactionRepository;
            _userRepository = userRepository;
            _topUpOptionsRepository = topUpOptionsRepository;
            _userBalanceInformationService = userBalanceInformationService;
            _topUpLimitOptionsRepository = topUpLimitOptionsRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<TransactionTopUpInformation> AddTopUpTransactionAsync(TransactionTopUpInformation transactionInformation)
        {
            var user = await _userRepository.GetUserById(transactionInformation.UserId, _cancellationTokenSource.Token);
            var topUpTransactionsPerBeneficiray = user.TopUpTransactions.Where(x => x.BeneficiaryId == transactionInformation.BeneficiaryId);
            var topUpOptionsDetails =await  _topUpOptionsRepository.GetTopUpOptionsById(transactionInformation.TopUpOptionId, _cancellationTokenSource.Token);

            //Get available User Balance
            var userBalance = await _userBalanceInformationService.GetUserBalanceInformationAsync(transactionInformation.UserId);
            if (userBalance.currentBalance < (topUpOptionsDetails.TopUpFee + topUpOptionsDetails.Amount))
            {
                string errorMessage = "User Balance is less than TopUp Amount";
                _logger.LogInformation(errorMessage);
                throw new HttpRequestException(errorMessage, null, System.Net.HttpStatusCode.PreconditionFailed);
            }

            //Validate TopUp Limit
            var isValidTransaction = await ValidateTransaction(transactionInformation.UserId, transactionInformation.BeneficiaryId, topUpOptionsDetails.Amount);

            if (isValidTransaction)
            {
                try
                {
                    //Update User Balance
                    await UpdateUserBalance(transactionInformation.UserId, topUpOptionsDetails.Amount);
                    var result = await _topUpTransactionRepository.AddTopUpTransaction(
                        new TopUpTransactionEntity
                        {
                            Amount = topUpOptionsDetails.Amount,
                            BeneficiaryId = transactionInformation.BeneficiaryId,
                            TopUpOptionId = transactionInformation.TopUpOptionId,
                            TransactionDate = DateTime.Now,
                            UserId = transactionInformation.UserId,
                            TopUpFee = topUpOptionsDetails.TopUpFee,
                            TransactionStatus = "Pending",
                            TransactionType = "TopUp",
                            TransactionRemarks = "TopUp Transaction"

                        },
                   _cancellationTokenSource.Token);
                    var test = _mapper.Map<TransactionTopUpInformation>(result);
                    return _mapper.Map<TransactionTopUpInformation>(test);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occured while adding beneficiary");
                    throw;
                }
            }
            else { 
                string errorMessage = "TopUp Limit Exceeded";
                _logger.LogInformation(errorMessage);
                throw new HttpRequestException(errorMessage, null, System.Net.HttpStatusCode.PreconditionFailed);
            }
        }
        private async Task<UserEntity> UpdateUserBalance(int userId, decimal amount)
        {
            var user = await _userRepository.GetUserById(userId, _cancellationTokenSource.Token);
            user.SessionBalance = user.SessionBalance - amount;
            //Call and forget till full syncyronization is Done and Balance are updated
            await _userBalanceInformationService.DebitUserBalanceAsync(new UserBalanceInformation { UserId = userId, currentBalance = amount });
            return await _userRepository.UpdateUser(user, _cancellationTokenSource.Token);

        }
        private async Task<bool> ValidateTransaction(int userId, int beneficiaryId, decimal amount)
        {
            var user = await _userRepository.GetUserById(userId, _cancellationTokenSource.Token);
            var topUpTransactionsPerBeneficiray = user.TopUpTransactions.Where(x => x.BeneficiaryId == beneficiaryId);
            var topUpOptionsDetails = await _topUpOptionsRepository.GetTopUpOptionsById(beneficiaryId, _cancellationTokenSource.Token);

            //Get available User Balance
            var userBalance = await _userBalanceInformationService.GetUserBalanceInformationAsync(userId);
            if (userBalance.currentBalance < (topUpOptionsDetails.TopUpFee + topUpOptionsDetails.Amount))
            {
                string errorMessage = "User Balance is less than TopUp Amount";
                _logger.LogInformation(errorMessage);
                throw new HttpRequestException(errorMessage, null, System.Net.HttpStatusCode.PreconditionFailed);
            }

            //Validate TopUp Limit
            //Top Up Limit Type Monthly Per one beneficiary in case of user is not verified
            if (!user.IsVerified)
            {
                await CheckRole(1, topUpTransactionsPerBeneficiray.ToList(), topUpOptionsDetails.Amount);
            }
            //Top Up Limit Type Monthly Per one beneficiary in case of user is verified
            else
            {
                await CheckRole(2, topUpTransactionsPerBeneficiray.ToList(), topUpOptionsDetails.Amount);
            }
            //Top Up Limit Type Monthly Per All beneficiaries
            await CheckRole(3, user.TopUpTransactions.ToList(), topUpOptionsDetails.Amount);
            return true;
        }
        private async Task<bool> CheckRole(int roleNumber, List<TopUpTransactionEntity> transactions, decimal newTopupValue) {
            var topUpLimitOptions = await _topUpLimitOptionsRepository.GetTopUpLimitOptions(_cancellationTokenSource.Token);
            var totalTransactionAmount = transactions.Where(x => x.TransactionDate.Month == DateTime.Now.Month).Sum(x => x.Amount);
            var topUpLimit = topUpLimitOptions.Where(x => x.Id == 1).FirstOrDefault()?.TopUpLimits.FirstOrDefault();
            if (topUpLimit != null && topUpLimit.TopUpLimit < (totalTransactionAmount + newTopupValue))
            {
                string errorMessage = topUpLimit.TopUpLimitType.Name + ", TopUp Limit Exceeded";
                _logger.LogInformation(errorMessage);
                throw new HttpRequestException(errorMessage, null, System.Net.HttpStatusCode.PreconditionFailed);
            }
            return true;
        }
    }
}
