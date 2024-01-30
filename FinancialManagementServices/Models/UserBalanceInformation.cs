namespace FinancialManagementServices.Models
{
    public class UserBalanceInformation
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public decimal UserBalance { get; set; }
    }
}