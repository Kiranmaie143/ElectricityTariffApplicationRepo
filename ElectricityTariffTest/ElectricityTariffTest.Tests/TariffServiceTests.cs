using ElectricityTariffTest.Server.Calculators;
using ElectricityTariffTest.Server.Models;
using ElectricityTariffTest.Server.Repositories;
using ElectricityTariffTest.Server.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityTariffTest.Tests
{
    [TestClass]
    public class TariffServiceTests
    {
        private Mock<ITariffRepository> _mockTariffRepository;
        private Mock<ILogger<TariffService>> _mockLogger;
        private List<ITariffCalculator> _calculators;
        private TariffService _tariffService;

        [TestInitialize]
        public void SetUp()
        {
            _mockTariffRepository = new Mock<ITariffRepository>();
            _mockLogger = new Mock<ILogger<TariffService>>();
            _calculators = new List<ITariffCalculator>
        {
            new BasicTariffCalculator(),
            new PackagedTariffCalculator()
        };
            _tariffService = new TariffService(_calculators, _mockTariffRepository.Object, _mockLogger.Object);
        }

        [TestMethod]
        public void CalculateCosts_ShouldCalculateCostForKnownTariffs()
        {
            // Arrange
            var tariffs = new List<Tariff>
    {
        new Tariff { Name = "Product A", Type = 1, BaseCost = 5, AdditionalKwhCost = 22 },
        new Tariff { Name = "Product B", Type = 2, IncludedKwh = 4000, BaseCost = 800, AdditionalKwhCost = 30 }
    };
            _mockTariffRepository.Setup(repo => repo.GetTariffs()).Returns(tariffs);

            // Act
            var result = _tariffService.CalculateCosts(5000);

            // Assert
            Assert.AreEqual(2, result.Count());

            // Verify that no errors were logged
            _mockLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true),  // We don't care about the state
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), // We don't care about the formatter
                Times.Never);
        }
    }
}
