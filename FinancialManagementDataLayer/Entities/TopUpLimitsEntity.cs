namespace FinancialManagementDataLayer.Entities
{
    public class TopUpLimitsEntity
    {
        public int Id { get; set; }
        public Decimal TopUpLimit { get; set; }
        public int TopUpLimitTypeId { get; set; }
        public TopUpLimitTypeEntity TopUpLimitType { get; set; }
    }
}
