using FinancialManagementServices.Models;

namespace FinancialBeneficiaries.ExternalServices
{
    public class UserBalanceInformationService : IUserBalanceInformationService
    {
        public  Task<UserBalanceInformation> GetUserBalanceInformationAsync(int userId)
        {
            UserBalanceInformation userBalanceInformation = new UserBalanceInformation{ 
                UserId = 1,
                UserName = "Test",
                UserEmail = "",
                UserBalance = 1000
            };
            return Task.FromResult(userBalanceInformation);
        }
    }
}
