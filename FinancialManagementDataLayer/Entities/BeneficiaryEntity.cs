namespace FinancialManagementDataLayer.Entities
{
    public class BeneficiaryEntity
    {
        public int BeneficiaryId { get; set; }
        public string BeneficiaryNicKName{ get; set; }
        public required string BeneficiaryName { get; set; }
        public string? BeneficiaryAccountNumber { get; set; }
        public required string BeneficiaryBankName { get; set; }
        public required string BeneficiaryBankBranch { get; set; }
        public required string BeneficiaryBankSwiftCode { get; set; }
        public string? BeneficiaryBankIban { get; set; }
        public string? BeneficiaryBankAccountCurrency { get; set; }
        public int UserId { get; set; }

        public bool IsActive { get; set; }
        public ICollection<TopUpTransactionEntity> TopUpTransactions { get; set; }
        public UserEntity User { get; set; }


    }
}
