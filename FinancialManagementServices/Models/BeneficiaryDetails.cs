using System.ComponentModel.DataAnnotations;

namespace FinancialManagementServices.Models
{
    public class BeneficiaryDetails
    {
        public int BeneficiaryId { get; set; }

        [StringLength(20)]
        public string BeneficiaryNicKName { get; set; }

        public string BeneficiaryName { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public string BeneficiaryBankName { get; set; }
        public string BeneficiaryBankBranch { get; set; }
        public string BeneficiaryBankSwiftCode { get; set; }
        public string BeneficiaryBankIban { get; set; }
        public string BeneficiaryBankAccountCurrency { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
    }
}