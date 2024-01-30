using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagementServices.Models;

namespace FinancialManagementServices.UserBeneficialServices
{
    public interface IUserDetailsManagementService
    {
        Task<UserDetails> GetUserByIdAsync(int userId);
    }
}
