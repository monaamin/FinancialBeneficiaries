using UserBalanceManagementService.DataLayer.Entities;

namespace UserBalanceManagementService.DataLayer.Repository
{
    public interface IUserBalanceRepository
    {
        Task<UserBalance> GetUserBalance(int userId);
        Task <UserBalance> UpdateUserBalance(UserBalance userBalance);

    }
}
