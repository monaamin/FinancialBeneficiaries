namespace FinancialManagementDataLayer.Entities
{
    public class TopUpTransactionEntity
    {
        public int Id { get; set; }
        public int TopUpLimitTypeId { get; set; }
        public decimal Amount { get; set; }
        public decimal TopUpFee { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; } = "unknown";
        public string TransactionStatus { get; set; } = "unknown";
        public string? TransactionRemarks { get; set; }
        public UserEntity? user { get; set;}
        public BeneficiaryEntity? beneficiary { get; set;}
        public int UserId { get; set; }
        public int BeneficiaryId { get; set; }
        public TopUpOptionsEntity? TopUpOption { get; set; }
        public int TopUpOptionId { get; set; }
    }
}
