using FinancialManagementDataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagementDataLayer.Repositories.Abstractions
{
    internal interface ITopUpTransactionRepository
    {
        Task<IEnumerable<TopUpTransactionEntity>> GetTopUpTransactionsByUserId(int userId, CancellationToken cancel);
        Task<IEnumerable<TopUpTransactionEntity>> GetTopUpTransactionsPerUserByBeneficieryId(int userId, int beneficeryId, CancellationToken cancel);
        Task<TopUpTransactionEntity> AddTopUpTransaction(TopUpTransactionEntity topUpTransaction, CancellationToken cancel);
    }
}
