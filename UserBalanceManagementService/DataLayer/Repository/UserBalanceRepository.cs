using Microsoft.EntityFrameworkCore;
using UserBalanceManagementService.DataLayer.Entities;

namespace UserBalanceManagementService.DataLayer.Repository
{
    public class UserBalanceRepository : IUserBalanceRepository
    {
        private readonly UserBalanceContext _context;
        public UserBalanceRepository(UserBalanceContext context)
        {
            _context = context;
        }
        public async Task<UserBalance> GetUserBalance(int userId)
        {
            return await _context.UserBalance.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public Task<UserBalance> UpdateUserBalance(UserBalance userBalance)
        {
            var result = _context.UserBalance.Update(userBalance);
            _context.SaveChanges();
            return Task.FromResult(result.Entity);
        }
    }
}
