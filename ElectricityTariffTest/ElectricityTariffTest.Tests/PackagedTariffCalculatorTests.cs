using ElectricityTariffTest.Server.Calculators;
using ElectricityTariffTest.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricityTariffTest.Tests
{
    [TestClass]
    public class PackagedTariffCalculatorTests
    {
        private PackagedTariffCalculator _calculator;

        [TestInitialize]
        public void SetUp()
        {
            _calculator = new PackagedTariffCalculator();
        }

        [TestMethod]
        public void CalculateAnnualCost_ShouldReturnBaseCost_WhenConsumptionIsBelowOrEqualToIncludedKwh()
        {
            // Arrange
            decimal consumption = 3500m; // 3500 kWh/year
            var tariff = new Tariff
            {
                BaseCost = 800m,        // €800 for up to 4000 kWh/year
                IncludedKwh = 4000m,
                AdditionalKwhCost = 30m // 30 cents per kWh above 4000 kWh
            };

            // Act
            decimal result = _calculator.CalculateAnnualCost(consumption, tariff);

            // Assert
            // Since the consumption is within the included limit, only base cost is applied.
            Assert.AreEqual(800m, result);
        }

        [TestMethod]
        public void CalculateAnnualCost_ShouldReturnCorrectCost_WhenConsumptionExceedsIncludedKwh()
        {
            // Arrange
            decimal consumption = 4500m; // 4500 kWh/year
            var tariff = new Tariff
            {
                BaseCost = 800m,         // €800 for up to 4000 kWh/year
                IncludedKwh = 4000m,
                AdditionalKwhCost = 30m  // 30 cents per kWh above 4000 kWh
            };

            // Act
            decimal result = _calculator.CalculateAnnualCost(consumption, tariff);

            // Assert
            // Base cost = 800 EUR, Additional cost = (500 * 0.30) = 150 EUR
            Assert.AreEqual(950m, result);
        }

        [TestMethod]
        public void CalculateAnnualCost_ShouldHandleZeroConsumption()
        {
            // Arrange
            decimal consumption = 0m; // 0 kWh/year
            var tariff = new Tariff
            {
                BaseCost = 800m,
                IncludedKwh = 4000m,
                AdditionalKwhCost = 30m
            };

            // Act
            decimal result = _calculator.CalculateAnnualCost(consumption, tariff);

            // Assert
            // Since no consumption, only base cost is applied.
            Assert.AreEqual(800m, result);
        }

        [TestMethod]
        public void CalculateAnnualCost_ShouldHandleZeroBaseCost()
        {
            // Arrange
            decimal consumption = 4500m;
            var tariff = new Tariff
            {
                BaseCost = 0m,            // No base cost
                IncludedKwh = 4000m,
                AdditionalKwhCost = 30m
            };

            // Act
            decimal result = _calculator.CalculateAnnualCost(consumption, tariff);

            // Assert
            // Additional cost = (500 * 0.30) = 150 EUR
            Assert.AreEqual(150m, result);
        }

        [TestMethod]
        public void CalculateAnnualCost_ShouldHandleEdgeCase_ExactlyAtIncludedKwh()
        {
            // Arrange
            decimal consumption = 4000m; // Exactly 4000 kWh/year
            var tariff = new Tariff
            {
                BaseCost = 800m,
                IncludedKwh = 4000m,
                AdditionalKwhCost = 30m
            };

            // Act
            decimal result = _calculator.CalculateAnnualCost(consumption, tariff);

            // Assert
            // Since the consumption is exactly equal to the included kWh, only base cost is applied.
            Assert.AreEqual(800m, result);
        }
    }
}
