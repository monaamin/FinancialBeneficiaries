using FinancialManagementServices.Models;
using FinancialManagementServices.UserBeneficialServices;
using Microsoft.AspNetCore.Mvc;

namespace FinancialBeneficiaries.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserManagementController : ControllerBase
    {

        private readonly ILogger<UserManagementController> _logger;
        private readonly IUserDetailsManagementService _userDetailsManagementService;
        private readonly IBeneficiaryManagementService _beneficiaryManagementService;

        public UserManagementController(ILogger<UserManagementController> logger, 
            IUserDetailsManagementService userDetailsManagementService
            ,IBeneficiaryManagementService beneficiaryManagementService)
        {
            _logger = logger;
            _userDetailsManagementService = userDetailsManagementService;
            _beneficiaryManagementService = beneficiaryManagementService;
        }

        [HttpGet(Name = "GetUserDetails")]
        public ActionResult<UserDetails> GetByUserId(int userId)
        {
            return _userDetailsManagementService.GetUserByIdAsync(userId).Result;
        }

        [HttpPost(Name = "AddUserBeneficiary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public ActionResult<BeneficiaryDetails>  AddUserBeneficiary(BeneficiaryDetails beneficiary)
        {
            try
            {
                return _beneficiaryManagementService.AddBeneficiaryAsync(beneficiary).Result;
            }
            
            catch (Exception ex)
            {
                if (ex.InnerException.GetType() == typeof(HttpRequestException)) {
                    _logger.LogInformation(ex, "Error occured while adding beneficiary");
                    return StatusCode(StatusCodes.Status412PreconditionFailed,ex.Message);
                }
                _logger.LogError(ex, "Error occured while adding beneficiary");
                throw;
            }
        }
    }
}
