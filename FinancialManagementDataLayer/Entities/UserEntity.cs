namespace FinancialManagementDataLayer.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Email { get; set; }
        public required string Password { get; set; }
        public bool IsVerified { get; set; }
        public ICollection<BeneficiaryEntity> Beneficiaries { get; set; }
        public ICollection<TopUpTransactionEntity> TopUpTransactions { get; set; }


    }
}
