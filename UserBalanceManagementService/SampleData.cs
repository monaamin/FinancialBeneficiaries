using UserBalanceManagementService.DataLayer;
using UserBalanceManagementService.DataLayer.Entities;

namespace UserBalanceManagementService
{
    public class SampleData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<UserBalanceContext>();
           
            if (context!=null &&!context.Database.EnsureCreated() && context.UserBalance.Any())
            {
                return;
            }
            var users = GetUsers().ToArray();
            context.UserBalance.AddRange(users);
            context.SaveChanges();

           
        }

        private static List<UserBalance> GetUsers()
        {
            return new List<UserBalance>
            {
                new UserBalance
                {
                    Id = 1,
                    CurrentBalance = 100,
                    UserId = 1
                },
                new UserBalance
                {
                    Id = 2,
                    CurrentBalance = 100,
                    UserId = 1

                }
            };
        }
    }
}
