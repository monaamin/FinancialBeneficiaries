using AutoMapper;
using FinancialManagementDataLayer.Entities;
using FinancialManagementDataLayer.Repositories;
using FinancialManagementServices.Models;
using FinancialManagementServices.UserBeneficialServices;
using Microsoft.Extensions.Logging;
using Moq;

namespace FinancialManagementServices.Tests
{
    [TestClass]
    public class BeneficiaryManagementServiceTests
    {
        private Mock<IBeneficiaryRepository> _beneficiaryRepositoryMock;
        private Mock<ILogger<BeneficiaryManagementService>> _loggerMock;
        private Mock<IMapper> _mapperMock;
        private BeneficiaryManagementService _beneficiaryManagementService;

        BeneficiaryEntity beneficiary = new BeneficiaryEntity
        {
            BeneficiaryId = 1,
            BeneficiaryNicKName = "NickName1",
            BeneficiaryName = "John Doe",
            BeneficiaryAccountNumber = "123456789",
            BeneficiaryBankName = "BankA",
            BeneficiaryBankBranch = "BranchX",
            BeneficiaryBankSwiftCode = "SWIFT123",
            BeneficiaryBankIban = "IBAN456",
            BeneficiaryBankAccountCurrency = "USD",
            UserId = 1001,
            IsActive = true,
            TopUpTransactions = new List<TopUpTransactionEntity>
            {
                new TopUpTransactionEntity { Id = 101, Amount = 500, TransactionDate = DateTime.Now.AddDays(-1) },
                new TopUpTransactionEntity { Id = 102, Amount = 1000, TransactionDate = DateTime.Now.AddDays(-2) }
            },
            User = new UserEntity
            {
                Id = 1001,
                Name = "user123",
                Email = "user123@example.com",
                Password = "password123",
                IsVerified = true,
            }
        };
        BeneficiaryDetails beneficiaryDetails = new BeneficiaryDetails
        {
            BeneficiaryId = 1,
            BeneficiaryNicKName = "NickName1",
            BeneficiaryName = "John Doe",
            BeneficiaryAccountNumber = "123456789",
            BeneficiaryBankName = "BankA",
            BeneficiaryBankBranch = "BranchX",
            BeneficiaryBankSwiftCode = "SWIFT123",
            BeneficiaryBankIban = "IBAN456",
            BeneficiaryBankAccountCurrency = "USD",
            UserId = 1001,
            IsActive = true
        };

        [TestInitialize]
        public void Initialize()
        {
            _beneficiaryRepositoryMock = new Mock<IBeneficiaryRepository>();
            _loggerMock = new Mock<ILogger<BeneficiaryManagementService>>();
            _mapperMock = new Mock<IMapper>();

            _beneficiaryManagementService = new BeneficiaryManagementService(
                _beneficiaryRepositoryMock.Object,
                _loggerMock.Object,
                _mapperMock.Object
            );
        }

        [TestMethod]
        public async Task AddBeneficiaryAsync_Success()
        {
            // Arrange
            var beneficiaryDetails = new BeneficiaryDetails
            {
                // Set properties for a valid beneficiary
            };

            var beneficiaries = new List<BeneficiaryEntity>(); // Mock repository data
            _beneficiaryRepositoryMock.Setup(repo => repo.GetBeneficiariesByUserId(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(beneficiaries);

            _beneficiaryRepositoryMock.Setup(repo => repo.AddBeneficiary(It.IsAny<BeneficiaryEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(beneficiary);

            _mapperMock.Setup(mapper => mapper.Map<BeneficiaryEntity>(It.IsAny<BeneficiaryDetails>()))
                .Returns(beneficiary);
            _mapperMock.Setup(mapper => mapper.Map<BeneficiaryDetails>(It.IsAny<BeneficiaryEntity>()))
                .Returns(beneficiaryDetails);

            // Act
            var result = await _beneficiaryManagementService.AddBeneficiaryAsync(beneficiaryDetails);

            // Assert
            Assert.IsNotNull(result);
            // Add more assertions based on your business logic
        }

        [TestMethod]
        public async Task AddBeneficiaryAsync_TooManyActiveBeneficiaries()
        {
            // Arrange
            

            var beneficiaries = new List<BeneficiaryEntity>
            {
               beneficiary,
               beneficiary,
               beneficiary,
               beneficiary,
               beneficiary
            }; 

            _beneficiaryRepositoryMock.Setup(repo => repo.GetBeneficiariesByUserId(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(beneficiaries);
            _mapperMock.Setup(mapper => mapper.Map<BeneficiaryDetails>(It.IsAny<BeneficiaryEntity>()))
               .Returns(beneficiaryDetails);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => _beneficiaryManagementService.AddBeneficiaryAsync(beneficiaryDetails));
        }

        [TestMethod]
        public async Task AddBeneficiaryAsync_RepositoryException()
        {
            // Arrange
            var beneficiaryDetails = new BeneficiaryDetails
            {
                // Set properties for a valid beneficiary
            };

            _beneficiaryRepositoryMock.Setup(repo => repo.GetBeneficiariesByUserId(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception("Simulated repository exception"));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => _beneficiaryManagementService.AddBeneficiaryAsync(beneficiaryDetails));
        }
    }
}
