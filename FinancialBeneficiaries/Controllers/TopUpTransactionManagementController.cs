using FinancialManagementServices.Models;
using FinancialManagementServices.UserBeneficialServices;
using Microsoft.AspNetCore.Mvc;

namespace FinancialBeneficiaries.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TopUpTransactionManagementController : ControllerBase
    {

        private readonly ILogger<TopUpTransactionManagementController> _logger;
        private readonly ITopUpOptionsManagementService _topUpOptionsManagementService;
        private readonly ITransactionManagementService _transactionManagementService;


        public TopUpTransactionManagementController(ILogger<TopUpTransactionManagementController> logger,
            ITopUpOptionsManagementService topUpOptionsManagementService,
            ITransactionManagementService transactionManagementService)
        {
            _logger = logger;
            _topUpOptionsManagementService = topUpOptionsManagementService;
            _transactionManagementService = transactionManagementService;
        }

        [HttpGet(Name = "GetTopUpOptions")]
        public ActionResult<List<TopUpOptions>> Get()
        {
            return _topUpOptionsManagementService.GetTopUpOptionsList().Result;
        }

        [HttpPost(Name = "AddTopUpTransaction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public ActionResult<TransactionTopUpInformation> AddTopUpTransaction(TransactionTopUpInformation transactionTopUpInformation)
        {
            try
            {
                var result = _transactionManagementService.AddTopUpTransactionAsync(transactionTopUpInformation).Result;
                return result;
            }

            catch (Exception ex)
            {
                if (ex.InnerException.GetType() == typeof(HttpRequestException))
                {
                    _logger.LogInformation(ex, "Error occured while adding beneficiary");
                    return StatusCode(StatusCodes.Status412PreconditionFailed, ex.Message);
                }
                _logger.LogError(ex, "Error occured while adding beneficiary");
                throw;
            }
        }

    }
}
