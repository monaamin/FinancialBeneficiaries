using FinancialManagementDataLayer.Entities;
using FinancialManagementDataLayer.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagementDataLayer.Repositories
{
    public class TopUpTransactionRepository: ITopUpTransactionRepository
    {
        private readonly FinancialManagementContext _context;

        public TopUpTransactionRepository(FinancialManagementContext context) {
            _context = context;
        }

        public async Task<TopUpTransactionEntity> AddTopUpTransaction(TopUpTransactionEntity topUpTransaction, CancellationToken cancel)
        {
            var result = _context.TopUpTransactions.AddAsync(topUpTransaction, cancel);
            await _context.SaveChangesAsync(cancel);
            return result.Result.Entity;
        }

        public async Task<IEnumerable<TopUpTransactionEntity>> GetTopUpTransactionsByUserId(int userId, CancellationToken cancel)
        {
            return await _context.TopUpTransactions.Where(t => t.UserId == userId).ToListAsync(cancel);
        }

        public async Task<IEnumerable<TopUpTransactionEntity>> GetTopUpTransactionsPerUserByBeneficieryId(int userId, int beneficeryId, CancellationToken cancel)
        {
            return await _context.TopUpTransactions.Where(t => t.UserId == userId && t.BeneficiaryId == beneficeryId).ToListAsync(cancel);
        }
    }
}
