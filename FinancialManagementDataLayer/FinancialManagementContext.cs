using FinancialManagementDataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagementDataLayer
{
    public class FinancialManagementContext : DbContext
    {
        public FinancialManagementContext(DbContextOptions<FinancialManagementContext> options) : base(options)
        {

        }
        public DbSet<BeneficiaryEntity> Beneficiaries { get; set; }
        public DbSet<TopUpLimitsEntity> TopUpLimits { get; set; }
        public DbSet<TopUpLimitTypeEntity> TopUpLimitTypes { get; set; }
        public DbSet<TopUpOptionsEntity> TopUpOptions { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TopUpTransactionEntity> TopUpTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(build =>
            {
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.Id).ValueGeneratedOnAdd();
                build.HasMany<BeneficiaryEntity>(s => s.Beneficiaries).WithOne(g => g.User).HasForeignKey(s => s.UserId);
            });
            modelBuilder.Entity<BeneficiaryEntity>(build =>
            {
                build.HasKey(entry => entry.BeneficiaryId);
                build.Property(entry => entry.BeneficiaryId).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<TopUpTransactionEntity>(build =>
            {
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.Id).ValueGeneratedOnAdd();
                build.HasOne<UserEntity>(s => s.user).WithMany(g => g.TopUpTransactions).HasForeignKey(s => s.UserId);
                build.HasOne<BeneficiaryEntity>(s => s.beneficiary).WithMany(g=>g.TopUpTransactions).HasForeignKey(s=>s.BeneficiaryId);

            });

            modelBuilder.Entity<TopUpLimitsEntity>(build =>
            {
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.Id).ValueGeneratedOnAdd();
                build.HasOne<TopUpLimitTypeEntity>(s => s.TopUpLimitType).WithMany(g => g.TopUpLimits).HasForeignKey(s => s.TopUpLimitTypeId);
            });

            modelBuilder.Entity<TopUpTransactionEntity>(build =>
            {
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.Id).ValueGeneratedOnAdd();
                build.HasOne<TopUpOptionsEntity>(s => s.TopUpOption).WithMany(g => g.TopUpTransaction).HasForeignKey(s => s.TopUpOptionId);
            });

        }
    }
}
