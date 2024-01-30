using FinancialManagementDataLayer.Entities;

namespace FinancialManagementDataLayer.Repositories.Abstractions
{
    public interface ITopUpLimitOptionsRepository
    {
        Task<IEnumerable<TopUpLimitTypeEntity>> GetTopUpLimitOptions(CancellationToken cancel);
    }
}
