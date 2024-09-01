using ElectricityTariffTest.Server.Models;

namespace ElectricityTariffTest.Server.Repositories
{
    public class TariffRepository : ITariffRepository
    {
        public IEnumerable<Tariff> GetTariffs()
        {
            // In the future, you might fetch tariffs from a database or external provider.
            return new List<Tariff>
            {
                new Tariff { Name = "Product A", Type = 1, BaseCost = 5, AdditionalKwhCost = 22 },
                new Tariff { Name = "Product B", Type = 2, BaseCost = 800, IncludedKwh = 4000, AdditionalKwhCost = 30 }
            };
        }
    }
}
