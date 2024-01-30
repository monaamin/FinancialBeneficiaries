namespace FinancialManagementDataLayer.Entities
{
    public class TopUpLimitTypeEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<TopUpLimitsEntity> TopUpLimits { get; set; }
    }
}
