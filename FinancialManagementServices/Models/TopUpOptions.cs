namespace FinancialManagementServices.Models
{
    public class TopUpOptions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TopUpAmount { get; set; }
        public decimal Fee { get; set; }
    }
}