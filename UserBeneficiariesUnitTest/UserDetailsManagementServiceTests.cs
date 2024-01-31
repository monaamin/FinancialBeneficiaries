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
    public class UserDetailsManagementServiceTests
    {
        [TestMethod]
        public async Task GetUserByIdAsync_ValidUserId_ReturnsUserDetails()
        {
            // Arrange
            int userId = 1;
            var cancellationTokenSource = new CancellationTokenSource();
            var userRepositoryMock = new Mock<IUserRepository>();
            var loggerMock = new Mock<ILogger<UserDetailsManagementService>>();
            var mapperMock = new Mock<IMapper>();

            var userDetailsManagementService = new UserDetailsManagementService(userRepositoryMock.Object, loggerMock.Object, mapperMock.Object);

            var userEntity = new UserEntity
            {
                Id = userId,
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "password123",
                IsVerified = true,
                Beneficiaries = new List<BeneficiaryEntity>(),
                TopUpTransactions = new List<TopUpTransactionEntity>(),
                SessionBalance = 100.0m
            };

            var expectedUserDetails = new UserDetails
            {
                Id = userId,
                Name = "John Doe",
                Email = "john.doe@example.com",
                Password = "password123",
                IsVerified = true,
                Beneficiaries = new List<BeneficiaryDetails>(),
                TopUpTransactions = new List<TransactionTopUpInformation>()
            };

            userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                             .ReturnsAsync(userEntity);

            mapperMock.Setup(mapper => mapper.Map<UserDetails>(userEntity))
                      .Returns(expectedUserDetails);

            // Act
            var result = await userDetailsManagementService.GetUserByIdAsync(userId);

            // Assert
            Assert.AreEqual(expectedUserDetails, result);
            userRepositoryMock.Verify(repo => repo.GetUserById(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
            mapperMock.Verify(mapper => mapper.Map<UserDetails>(userEntity), Times.Once);
        }

        [TestMethod]
        public async Task GetUserByIdAsync_ExceptionOccurs_LoggerLogsErrorAndThrowsException()
        {
            // Arrange
            int userId = 1;
            var cancellationTokenSource = new CancellationTokenSource();
            var userRepositoryMock = new Mock<IUserRepository>();
            var loggerMock = new Mock<ILogger<UserDetailsManagementService>>();
            var mapperMock = new Mock<IMapper>();

            var userDetailsManagementService = new UserDetailsManagementService(userRepositoryMock.Object, loggerMock.Object, mapperMock.Object);

            userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                             .ThrowsAsync(new Exception("Simulated exception"));

            // Act and Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await userDetailsManagementService.GetUserByIdAsync(userId));

        }
    }
}
