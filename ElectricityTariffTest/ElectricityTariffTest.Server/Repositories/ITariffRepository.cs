using ElectricityTariffTest.Server.Models;

namespace ElectricityTariffTest.Server.Repositories
{
    public interface ITariffRepository
    {
        // Method to get all tariffs
        IEnumerable<Tariff> GetTariffs();
    }
}
