using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagementDataLayer.Entities
{
    public class TopUpTransactionEntity
    {
        public int Id { get; set; }
        public int TopUpLimitTypeId { get; set; }
        public int TopUpOptionsId { get; set; }
        public int Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public BeneficiaryEntity Beneficiary { get; set; }
        public UserEntity TransactionBy { get; set; }
        public string TransactionType { get; set; }
        public string TransactionStatus { get; set; }
        public string TransactionRemarks { get; set; }
        public UserEntity user { get; set;}
        public BeneficiaryEntity beneficiary { get; set;}
        public int UserId { get; set; }
        public int BeneficiaryId { get; set; }
        public TopUpOptionsEntity TopUpOption { get; set; }
        public int TopUpOptionId { get; set; }
    }
}
