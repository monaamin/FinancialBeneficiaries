using FinancialManagementDataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagementDataLayer.Repositories
{
    public class TopUpOptionsRepository : ITopUpOptionsRepository
    {
        private readonly FinancialManagementContext _context;

        public TopUpOptionsRepository(FinancialManagementContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TopUpOptionsEntity>> GetTopUpOptions(CancellationToken cancel)
        {
            return await _context.TopUpOptions.ToListAsync(cancel);
        }
    }
}
