using FinancialManagementDataLayer.Entities;

namespace FinancialManagementDataLayer.Repositories
{
    public interface ITopUpOptionsRepository
    {
        Task<IEnumerable<TopUpOptionsEntity>> GetTopUpOptions(CancellationToken cancel);
        Task<TopUpOptionsEntity> GetTopUpOptionsById(int topOptionId, CancellationToken cancel);
    }
}
