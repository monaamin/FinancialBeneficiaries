using FinancialManagementDataLayer.Entities;

namespace FinancialManagementDataLayer
{
    public class SampleData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<FinancialManagementContext>();
           
            if (context!=null &&!context.Database.EnsureCreated() && context.Beneficiaries.Any())
            {
                return;
            }
            var users = GetUsers().ToArray();
            context.Users.AddRange(users);
            var beneficiaries = GetBeneficiaries().ToArray();
            context.Beneficiaries.AddRange(beneficiaries);
            context.SaveChanges();
            var topUpOptions = GetTopUpOptions().ToArray();
            context.TopUpOptions.AddRange(topUpOptions);
            context.SaveChanges();
            var topUpLimitTypes = GetTopUpLimitTypes().ToArray();
            context.TopUpLimitTypes.AddRange(topUpLimitTypes);
            context.SaveChanges();
            var topUpLimits = GetTopUpLimits().ToArray();
            context.TopUpLimits.AddRange(topUpLimits);
            context.SaveChanges();
            var test = context.TopUpLimits.ToList();
        }

        private static List<UserEntity> GetUsers()
        {
            return new List<UserEntity>
            {
                new UserEntity
                {
                    Id = 1,
                    Name = "John",
                    Password = "123456",
                    Email = ""
                }
            };
        }

        private static List<TopUpLimitsEntity> GetTopUpLimits()
        {
            return new List<TopUpLimitsEntity>
            {
                new TopUpLimitsEntity
                {
                    Id = 1,
                    TopUpLimit = 500,
                    TopUpLimitTypeId = 1,
                },
                new TopUpLimitsEntity
                {
                    Id = 2,
                    TopUpLimit = 1000,
                    TopUpLimitTypeId = 2,
                },

            };
        }

        private static List<TopUpLimitTypeEntity> GetTopUpLimitTypes()
        {
            return new List<TopUpLimitTypeEntity>
            {
                new TopUpLimitTypeEntity
                {
                    Id = 1,
                    Name = "Daily Per One Beneficery",
                },
                new TopUpLimitTypeEntity
                {
                    Id = 2,
                    Name = "Daily Per All Beneficeries",
                },
                new TopUpLimitTypeEntity
                {
                    Id = 3,
                    Name = "Monthly Per One Beneficery",
                },
                  new TopUpLimitTypeEntity
                {
                    Id = 4,
                    Name = "Monthly Per All Beneficeries",
                },
            };
        }

        private static List<TopUpOptionsEntity> GetTopUpOptions()
        {
            return new List<TopUpOptionsEntity>
            {
                new TopUpOptionsEntity
                {
                    Id = 1,
                    Name = "AED 5",
                    Amount = 5,
                    TopUpFee = 1,
                },
                new TopUpOptionsEntity
                {
                    Id = 2,
                    Name = "AED 10",
                    Amount = 10,
                    TopUpFee = 1,
                },
                 new TopUpOptionsEntity
                {
                    Id = 3,
                    Name = "AED 20",
                    Amount = 20,
                    TopUpFee = 1,
                },
                  new TopUpOptionsEntity
                {
                    Id = 4,
                    Name = "AED 30",
                    TopUpFee = 1,
                    Amount = 30,
                },
                  new TopUpOptionsEntity
                  {
                      Id = 5,
                      Name = "AED 50",
                      Amount = 50,
                      TopUpFee = 1,
                 },
                 new TopUpOptionsEntity
                  {
                      Id = 6,
                      Name = "AED 75",
                      Amount = 75,
                      TopUpFee = 1,
                 },
                      new TopUpOptionsEntity
                      {
                      Id = 7,
                      Name = "AED 100",
                      Amount = 100,
                      TopUpFee = 1,
                 },
            };
        }

        private static List<BeneficiaryEntity> GetBeneficiaries()
        {
            return new List<BeneficiaryEntity>
            {
                new BeneficiaryEntity
                {
                    BeneficiaryId = 1,
                    BeneficiaryName = "John",
                    BeneficiaryAccountNumber = "123456789",
                    BeneficiaryBankName = "Emirates NBD",
                    BeneficiaryBankBranch = "Dubai",
                    BeneficiaryBankSwiftCode = "EBILAEAD",
                    BeneficiaryBankIban = "AE123456789",
                    BeneficiaryBankAccountCurrency = "AED",
                    UserId = 1,
                    BeneficiaryNicKName = "John",
                }
            };
        }
    }
}
