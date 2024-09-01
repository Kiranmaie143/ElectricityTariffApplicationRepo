namespace ElectricityTariffTest.Server.Models
{
    public class TariffCalculator
    {
        public static List<TariffCost> CalculateAnnualCosts(List<Tariff> tariffs, decimal consumption)
        {
            var tariffCosts = new List<TariffCost>();

            foreach (var tariff in tariffs)
            {
                decimal annualCost = 0;
                switch (tariff.Type)
                {
                    case 1: // Type 1 - Basic electricity tariff
                        annualCost = (tariff.BaseCost * 12) + (consumption * (tariff.AdditionalKwhCost / 100));
                        break;

                    case 2: // Type 2 - Packaged tariff
                        if (consumption <= tariff.IncludedKwh)
                        {
                            annualCost = tariff.BaseCost;
                        }
                        else
                        {
                            var extraKwh = consumption - tariff.IncludedKwh;
                            annualCost = tariff.BaseCost + (extraKwh * (tariff.AdditionalKwhCost / 100));
                        }
                        break;
                }

                tariffCosts.Add(new TariffCost
                {
                    Name = tariff.Name,
                    AnnualCost = Math.Round(annualCost, 2)
                });
            }

            return tariffCosts;
        }
    }
}
