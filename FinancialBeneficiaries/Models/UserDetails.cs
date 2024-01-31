namespace FinancialManagementServices.Models
{
    public class UserDetails
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Email { get; set; }
        public required string Password { get; set; }
        public bool IsVerified { get; set; }
        public List<BeneficiaryDetails> Beneficiaries { get; set; }
        public List<TransactionTopUpInformation> TopUpTransactions { get; set; }
    }
}