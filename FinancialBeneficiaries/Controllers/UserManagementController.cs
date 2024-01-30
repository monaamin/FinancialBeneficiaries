using FinancialManagementServices.Models;
using FinancialManagementServices.UserBeneficialServices;
using Microsoft.AspNetCore.Mvc;

namespace FinancialBeneficiaries.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : ControllerBase
    {

        private readonly ILogger<UserManagementController> _logger;
        private readonly IUserDetailsManagementService _userDetailsManagementService;
        public UserManagementController(ILogger<UserManagementController> logger, IUserDetailsManagementService userDetailsManagementService)
        {
            _logger = logger;
            _userDetailsManagementService = userDetailsManagementService;
        }

        [HttpGet(Name = "GetUserDetails")]
        public UserDetails GetByUserId(int userId)
        {
            return _userDetailsManagementService.GetUserByIdAsync(userId).Result;
        }
    }
}
