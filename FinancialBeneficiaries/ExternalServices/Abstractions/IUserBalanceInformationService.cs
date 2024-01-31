using FinancialManagementServices.Models;

namespace FinancialManagementServices.ExternalServices
{
    public interface IUserBalanceInformationService
    {
        Task<UserBalanceInformation> GetUserBalanceInformationAsync(int userId);
        Task DebitUserBalanceAsync(UserBalanceInformation debitInfo);
    }
}
