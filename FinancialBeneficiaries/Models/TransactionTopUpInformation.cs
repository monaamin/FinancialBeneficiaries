namespace FinancialManagementServices.Models
{
    public class TransactionTopUpInformation
    {
        public int UserId { get; set; }
        public int TopUpOptionId { get; set; }
        public int BeneficiaryId { get; set; }
    }
}