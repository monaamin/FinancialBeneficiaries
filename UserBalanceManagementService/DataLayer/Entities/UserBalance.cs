namespace UserBalanceManagementService.DataLayer.Entities
{
    public class UserBalance
    {
        public int Id { get; set; }
        public decimal CurrentBalance { get; set; }
        public int UserId { get; set; }
    }
}
