using Microsoft.AspNetCore.Mvc;
using UserBalanceManagementService.DataLayer.Entities;
using UserBalanceManagementService.DataLayer.Repository;
using UserBalanceManagementService.Models;

namespace UserBalanceManagementService.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class UserBalanceController : ControllerBase
    {
        private readonly ILogger<UserBalanceController> _logger;
        private readonly IUserBalanceRepository _userBalanceRepository;
        public UserBalanceController(ILogger<UserBalanceController> logger, IUserBalanceRepository userBalanceRepository)
        {
            _logger = logger;
            _userBalanceRepository = userBalanceRepository;
        }

        [HttpGet(Name = "GetUserBalance")]
        public ActionResult<UserBalance> GetUserBalance(int userId)
        {
            try {
                return _userBalanceRepository.GetUserBalance(userId).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting user balance");
                throw;
            }
            
        }

        [HttpPost(Name = "CreditUserBalance")]
        public ActionResult<UserBalance> CreditUserBalance(UserBalanceModel userModel)
        {
            try {
                var user = _userBalanceRepository.GetUserBalance(userModel.UserId).Result;

                user.CurrentBalance = user.CurrentBalance + userModel.Amount;
                 return _userBalanceRepository.UpdateUserBalance(user).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while adding user balance");
                throw;
            }

        }
        [HttpPost(Name = "DebitUserBalance")]
        public ActionResult<UserBalance> DebitUserBalance(UserBalanceModel userModel)
        {
            try
            {
                var user = _userBalanceRepository.GetUserBalance(userModel.UserId).Result;
                if (user.CurrentBalance < userModel.Amount) {
                    string message = "Insufficient balance";
                    _logger.LogInformation(message);
                    return StatusCode(StatusCodes.Status412PreconditionFailed, message);
                }
            
                user.CurrentBalance = user.CurrentBalance - userModel.Amount;
                return _userBalanceRepository.UpdateUserBalance(user).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while adding user balance");
                throw;
            }

        }
    }
}
