namespace FinancialManagementServices.Models
{
    public class TransactionTopUpInformation
    {
        public int UserId { get; set; }
        public int TopUpOption { get; set; }
        public int BeneficiaryId { get; set; }
    }
}