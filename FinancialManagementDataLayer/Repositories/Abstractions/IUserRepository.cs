using FinancialManagementDataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagementDataLayer.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUserById(int userId, CancellationToken cancel);
        Task<UserEntity> GetUserByEmail(string email, CancellationToken cancel);
        Task<UserEntity> AddUser(UserEntity user, CancellationToken cancel);
        Task<UserEntity> UpdateUser(UserEntity user, CancellationToken cancel);
    }
}
