using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagementDataLayer.Entities
{
    public class TopUpOptionsEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<TopUpTransactionEntity> TopUpTransactions { get; set; }
        public Double Amount { get; set; }
        public Double TopUpFee { get; set; }
        public ICollection< TopUpTransactionEntity> TopUpTransaction { get; set; }
    }
}
