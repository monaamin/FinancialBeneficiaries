using FinancialManagementDataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagementDataLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FinancialManagementContext _context;

        public UserRepository(FinancialManagementContext context)
        {
            _context = context;
        }
        public Task<UserEntity> AddUser(UserEntity user, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> GetUserByEmail(string email, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity> GetUserById(int userId, CancellationToken cancel)
        {
            return await _context.Users.Include(x => x.Beneficiaries).FirstOrDefaultAsync(x => x.Id == userId, cancel);
        }

        public Task<UserEntity> UpdateUser(UserEntity user, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }
    }
}
