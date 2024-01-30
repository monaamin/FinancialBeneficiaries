using FinancialManagementDataLayer.Entities;

namespace FinancialManagementDataLayer.Repositories.Abstractions
{
    public interface ITopUpTransactionRepository
    {
        Task<IEnumerable<TopUpTransactionEntity>> GetTopUpTransactionsByUserId(int userId, CancellationToken cancel);
        Task<IEnumerable<TopUpTransactionEntity>> GetTopUpTransactionsPerUserByBeneficieryId(int userId, int beneficeryId, CancellationToken cancel);
        Task<TopUpTransactionEntity> AddTopUpTransaction(TopUpTransactionEntity topUpTransaction, CancellationToken cancel);
    }
}
