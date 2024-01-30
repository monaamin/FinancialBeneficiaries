namespace FinancialManagementDataLayer.Entities
{
    public class TopUpOptionsEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal TopUpFee { get; set; }
        public ICollection< TopUpTransactionEntity> TopUpTransaction { get; set; }
    }
}
