using Microsoft.EntityFrameworkCore;
using UserBalanceManagementService.DataLayer.Entities;

namespace UserBalanceManagementService.DataLayer
{
    public class UserBalanceContext : DbContext
    {
        public UserBalanceContext(DbContextOptions<UserBalanceContext> options) : base(options)
        {

        }
       
        public DbSet<UserBalance> UserBalance { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserBalance>(build =>
            {
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.Id).ValueGeneratedOnAdd();
            });
        }
    
    }
}
