using ElectricityTariffTest.Server.Models;

namespace ElectricityTariffTest.Server.Calculators
{
    public class BasicTariffCalculator : ITariffCalculator
    {
        public bool CanCalculate(int tariffType)
        {
            // Basic tariffs are identified by type 1
            return tariffType == 1;
        }

        public decimal CalculateAnnualCost(decimal consumption, Tariff tariff)
        {
            // Use GetValueOrDefault() to safely handle nullable decimal values
            return tariff.BaseCost * 12 + consumption * tariff.AdditionalKwhCost / 100;
        }
    }
}
