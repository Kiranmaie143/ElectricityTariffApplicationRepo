using ElectricityTariffTest.Server.Calculators;
using ElectricityTariffTest.Server.DTO;
using ElectricityTariffTest.Server.Interfaces;
using ElectricityTariffTest.Server.Repositories;

namespace ElectricityTariffTest.Server.Services
{
    public class TariffService : ITariffService
    {
        private readonly IEnumerable<ITariffCalculator> _calculators;
        private readonly ITariffRepository _tariffRepository;
        private readonly ILogger<TariffService> _logger; // Inject logger

        public TariffService(IEnumerable<ITariffCalculator> calculators, ITariffRepository tariffRepository, ILogger<TariffService> logger)
        {
            _calculators = calculators ?? throw new ArgumentNullException(nameof(calculators));
            _tariffRepository = tariffRepository ?? throw new ArgumentNullException(nameof(tariffRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public IEnumerable<TariffDto> CalculateCosts(decimal consumption)
        {
            var result = new List<TariffDto>();

            var tariffs = _tariffRepository.GetTariffs();
            try
            {
                if (tariffs == null || !tariffs.Any())
                {
                    _logger.LogWarning("No tariffs found in the repository.");
                    return result;
                }

                foreach (var tariff in tariffs)
                {
                    var calculator = _calculators.FirstOrDefault(c => c.CanCalculate(tariff.Type));
                    if (calculator != null)
                    {
                        var annualCost = calculator.CalculateAnnualCost(consumption, tariff);
                        result.Add(new TariffDto { TariffName = tariff.Name, AnnualCost = annualCost });
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while calculating costs.");
            }

            return result;
        }
    }
}
