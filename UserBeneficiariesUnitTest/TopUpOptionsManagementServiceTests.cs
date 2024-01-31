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
    public class TopUpOptionsManagementServiceTests
    {
        private Mock<ITopUpOptionsRepository> _topUpOptionsRepositoryMock;
        private Mock<ILogger<TopUpOptionsManagementService>> _loggerMock;
        private Mock<IMapper> _mapperMock;
        private TopUpOptionsManagementService _topUpOptionsManagementService;

        [TestInitialize]
        public void Initialize()
        {
            _topUpOptionsRepositoryMock = new Mock<ITopUpOptionsRepository>();
            _loggerMock = new Mock<ILogger<TopUpOptionsManagementService>>();
            _mapperMock = new Mock<IMapper>();

            _topUpOptionsManagementService = new TopUpOptionsManagementService(
                _topUpOptionsRepositoryMock.Object,
                _loggerMock.Object,
                _mapperMock.Object
            );
        }

        [TestMethod]
        public async Task GetTopUpOptionsList_Success()
        {
            // Arrange
            var topUpOptionsFromRepository = new List<TopUpOptions>
            {
                new TopUpOptions { Id = 1, Name = "OptionA", TopUpAmount = 50, Fee = 2.5M },
                new TopUpOptions { Id = 2, Name = "OptionB", TopUpAmount = 100, Fee = 5.0M }
            };
            var topUpOptionsFromService = new List<TopUpOptionsEntity>
            {
                new TopUpOptionsEntity { Id = 1, Name = "OptionA", Amount = 50, TopUpFee = 2.5M },
                new TopUpOptionsEntity { Id = 2, Name = "OptionB", Amount = 100, TopUpFee = 5.0M }
            };

            _topUpOptionsRepositoryMock.Setup(repo => repo.GetTopUpOptions(It.IsAny<CancellationToken>()))
                .ReturnsAsync(topUpOptionsFromService);

            _mapperMock.Setup(mapper => mapper.Map<List<TopUpOptions>>(It.IsAny<IEnumerable<FinancialManagementDataLayer.Entities.TopUpOptionsEntity>>()))
                .Returns(topUpOptionsFromRepository);

            // Act
            var result = await _topUpOptionsManagementService.GetTopUpOptionsList();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(topUpOptionsFromRepository.Count, result.Count);
            // Add more assertions based on your business logic
        }

        [TestMethod]
        public async Task GetTopUpOptionsList_RepositoryException()
        {
            // Arrange
            _topUpOptionsRepositoryMock.Setup(repo => repo.GetTopUpOptions(It.IsAny<CancellationToken>()))
                .Throws(new Exception("Simulated repository exception"));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => _topUpOptionsManagementService.GetTopUpOptionsList());
        }
    }
}
