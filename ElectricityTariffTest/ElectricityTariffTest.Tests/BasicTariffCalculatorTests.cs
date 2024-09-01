using ElectricityTariffTest.Server.Calculators;
using ElectricityTariffTest.Server.Models;

namespace ElectricityTariffTest.Tests
{
    [TestClass]
    public class BasicTariffCalculatorTests
    {
        private BasicTariffCalculator _calculator;

        [TestInitialize]
        public void SetUp()
        {
            _calculator = new BasicTariffCalculator();
        }
        [TestMethod]
        public void CalculateAnnualCost_ShouldReturnCorrectCost_ForGivenConsumptionAndTariff()
        {
            // Arrange
            decimal consumption = 5000m; // 5000 kWh/year
            var tariff = new Tariff
            {
                BaseCost = 5m, // 5 EUR/month
                AdditionalKwhCost = 22m // 22 cents per kWh
            };

            // Act
            decimal result = _calculator.CalculateAnnualCost(consumption, tariff);

            // Assert
            // Expected result: Base cost = 5 * 12 = 60 EUR
            // Consumption cost = 5000 * 0.22 = 1100 EUR
            // Total = 60 + 1100 = 1160 EUR
            Assert.AreEqual(1160m, result);
        }

        [TestMethod]
        public void CalculateAnnualCost_ShouldHandleZeroConsumption()
        {
            // Arrange
            decimal consumption = 0m; // 0 kWh/year
            var tariff = new Tariff
            {
                BaseCost = 5m,
                AdditionalKwhCost = 22m
            };

            // Act
            decimal result = _calculator.CalculateAnnualCost(consumption, tariff);

            // Assert
            // Base cost = 5 * 12 = 60 EUR, no additional consumption cost
            Assert.AreEqual(60m, result);
        }

        [TestMethod]
        public void CalculateAnnualCost_ShouldHandleZeroBaseCost()
        {
            // Arrange
            decimal consumption = 5000m;
            var tariff = new Tariff
            {
                BaseCost = 0m,
                AdditionalKwhCost = 22m
            };

            // Act
            decimal result = _calculator.CalculateAnnualCost(consumption, tariff);

            // Assert
            // Consumption cost = 5000 * 0.22 = 1100 EUR, no base cost
            Assert.AreEqual(1100m, result);
        }
    }
}