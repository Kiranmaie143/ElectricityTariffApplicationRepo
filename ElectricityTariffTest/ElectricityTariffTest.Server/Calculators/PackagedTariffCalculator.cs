using ElectricityTariffTest.Server.Models;

namespace ElectricityTariffTest.Server.Calculators
{
    public class PackagedTariffCalculator :  ITariffCalculator
    {
        public bool CanCalculate(int tariffType)
        {
            // Packaged tariffs are identified by type 2
            return tariffType == 2;
        }
        public decimal CalculateAnnualCost(decimal consumption, Tariff tariff)
        {
            return consumption <= tariff.IncludedKwh
                ? tariff.BaseCost
                : tariff.BaseCost + (consumption - tariff.IncludedKwh) * tariff.AdditionalKwhCost / 100;
        }
    }
}
