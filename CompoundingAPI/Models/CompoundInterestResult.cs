
namespace CompoundingAPI.Models
{
    public class CompoundInterestResult
    {
        public int TotalYears { get; set; }
        public double TotalContributions { get; set; }
        public double TotalInterestAfterTax { get; set; }
        public double FinalBalance { get; set; }
        public double InflationAdjustedBalance { get; set; }
        public double TotalInterestBeforeTax { get; set; }
    }
}
