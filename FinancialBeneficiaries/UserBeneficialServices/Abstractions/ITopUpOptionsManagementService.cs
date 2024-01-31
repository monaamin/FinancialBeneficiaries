using FinancialManagementServices.Models;

namespace FinancialManagementServices.UserBeneficialServices
{
    public interface ITopUpOptionsManagementService
    {
        Task<List<TopUpOptions>> GetTopUpOptionsList();
    }
}
