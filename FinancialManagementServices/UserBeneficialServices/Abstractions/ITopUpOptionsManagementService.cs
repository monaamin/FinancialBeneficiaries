using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManagementServices.Models;

namespace FinancialManagementServices.UserBeneficialServices
{
    internal interface ITopUpOptionsManagementService
    {
        Task<TopUpOptions> GetTopUpOptionsList();
    }
}
