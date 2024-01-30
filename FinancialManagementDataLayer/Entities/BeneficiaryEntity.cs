using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagementDataLayer.Entities
{
    public class BeneficiaryEntity
    {

        public int BeneficiaryId { get; set; }
        public required string BeneficiaryName { get; set; }
        public string? BeneficiaryAccountNumber { get; set; }
        public required string BeneficiaryBankName { get; set; }
        public required string BeneficiaryBankBranch { get; set; }
        public required string BeneficiaryBankSwiftCode { get; set; }
        public string? BeneficiaryBankIban { get; set; }
        public string? BeneficiaryBankAccountCurrency { get; set; }
        public UserEntity User { get; set; }
        public int UserId { get; set; }

        internal ICollection<TopUpTransactionEntity> TopUpTransactions;

    }
}
