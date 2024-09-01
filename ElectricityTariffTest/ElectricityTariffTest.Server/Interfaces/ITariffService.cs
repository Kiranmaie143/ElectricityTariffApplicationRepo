using ElectricityTariffTest.Server.DTO;

namespace ElectricityTariffTest.Server.Interfaces
{
    public interface ITariffService
    {
        IEnumerable<TariffDto> CalculateCosts(decimal consumption);
    }
}
