using FinancialManagementServices.Models;

namespace FinancialManagementServices.UserBeneficialServices
{
    public interface IUserDetailsManagementService
    {
        Task<UserDetails> GetUserByIdAsync(int userId);
    }
}
