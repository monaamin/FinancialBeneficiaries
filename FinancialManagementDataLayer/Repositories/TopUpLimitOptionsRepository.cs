using FinancialManagementDataLayer.Entities;
using FinancialManagementDataLayer.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagementDataLayer.Repositories
{
    public class TopUpLimitOptionsRepository : ITopUpLimitOptionsRepository
    {
        private readonly FinancialManagementContext _context;

        public TopUpLimitOptionsRepository(FinancialManagementContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TopUpLimitTypeEntity>> GetTopUpLimitOptions(CancellationToken cancel)
        {
            return await _context.TopUpLimitTypes.Include(x=>x.TopUpLimits).ToListAsync(cancel);
        }
    }
}
