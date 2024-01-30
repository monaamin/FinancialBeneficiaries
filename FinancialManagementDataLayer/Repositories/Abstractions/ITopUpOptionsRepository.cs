using FinancialManagementDataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagementDataLayer.Repositories
{
    public interface ITopUpOptionsRepository
    {
        Task<IEnumerable<TopUpOptionsEntity>> GetTopUpOptions(CancellationToken cancel);
    }
}
