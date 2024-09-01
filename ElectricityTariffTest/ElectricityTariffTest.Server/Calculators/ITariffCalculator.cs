using ElectricityTariffTest.Server.Models;

namespace ElectricityTariffTest.Server.Calculators
{
    public interface ITariffCalculator
    {
        // Determines if the calculator can calculate the tariff for the given type
        bool CanCalculate(int tariffType);
        decimal CalculateAnnualCost(decimal consumption, Tariff tariff);
    }
}
