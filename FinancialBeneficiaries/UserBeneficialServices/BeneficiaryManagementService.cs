using AutoMapper;
using FinancialManagementDataLayer.Repositories;
using FinancialManagementServices.Models;
using Microsoft.Extensions.Logging;

namespace FinancialManagementServices.UserBeneficialServices
{
    public class BeneficiaryManagementService : IBeneficiaryManagementService
    {
        private readonly IBeneficiaryRepository _beneficiaryRepository;
        private readonly ILogger<BeneficiaryManagementService> _logger;
        private readonly IMapper _mapper;
        CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public BeneficiaryManagementService(IBeneficiaryRepository beneficiaryRepository, ILogger<BeneficiaryManagementService> logger, IMapper mapper)
        {
            _beneficiaryRepository = beneficiaryRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<BeneficiaryDetails> AddBeneficiaryAsync(BeneficiaryDetails beneficiaryDetails)
        {
            //Get User Beneficiaries List
            var beneficiaries = await _beneficiaryRepository.GetBeneficiariesByUserId(beneficiaryDetails.UserId, _cancellationTokenSource.Token);
            //Check if Beneficiaries Count is less than 5
            var activeBeneficiaries = beneficiaries.Where(x => x.IsActive == true);
            if (beneficiaries.Any() && activeBeneficiaries.Count() >= 5) {
                string errorMessage = "Can Not Add More than 5 active beneficiaries ";
                _logger.LogInformation(errorMessage);
                throw new HttpRequestException(errorMessage,null, System.Net.HttpStatusCode.PreconditionFailed);
            }
            
            try
            {
                var beneficiary = _mapper.Map<FinancialManagementDataLayer.Entities.BeneficiaryEntity>(beneficiaryDetails);
                var addedBeneficiary = await _beneficiaryRepository.AddBeneficiary(beneficiary, _cancellationTokenSource.Token);
                return _mapper.Map<BeneficiaryDetails>(addedBeneficiary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while adding beneficiary");
                throw;
            }
            
        }
    }
}
