namespace ElectricityTariffTest.Server.Models
{
    public class Tariff
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public decimal BaseCost { get; set; }
        public decimal AdditionalKwhCost { get; set; }
        public decimal IncludedKwh { get; set; }
    }
    public class TariffCost
    {
        public string Name { get; set; }
        public decimal AnnualCost { get; set; }
    }
}
