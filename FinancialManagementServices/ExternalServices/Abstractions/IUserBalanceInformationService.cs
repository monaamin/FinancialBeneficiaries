using FinancialManagementServices.Models;

namespace FinancialBeneficiaries.ExternalServices
{
    public interface IUserBalanceInformationService
    {
        Task<UserBalanceInformation> GetUserBalanceInformationAsync(int userId);
    }
}
