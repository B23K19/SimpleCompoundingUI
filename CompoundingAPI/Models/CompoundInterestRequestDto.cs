namespace CompoundingAPI.Models
{

    public class CompoundInterestRequestDto
    {
        public double InitialInvestment { get; set; }
        public double Rate { get; set; }
        public bool MakeContributions { get; set; }
        public double Contribution { get; set; }
        public int ContributionFrequency { get; set; }
        public int Years { get; set; }
        public int CompoundFrequency { get; set; }
        public double InflationRate { get; set; }
        public double TaxRate { get; set; }
    }
}
