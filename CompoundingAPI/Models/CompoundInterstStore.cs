using System.ComponentModel.DataAnnotations.Schema;

namespace CompoundingAPI.Models
{
	[Table("store_values")]
	public class CompoundInterestStore
	{
		[Column("id")]
		public int Id { get; set; }

		[Column("initial_investment")]
		public decimal InitialInvestment { get; set; }

		[Column("rate")]
		public decimal Rate { get; set; }

		[Column("make_contributions")]
		public bool MakeContributions { get; set; }

		[Column("contribution")]
		public decimal Contribution { get; set; }

		[Column("contribution_frequency")]
		public int ContributionFrequency { get; set; }

		[Column("years")]
		public int Years { get; set; }

		[Column("compound_frequency")]
		public int CompoundFrequency { get; set; }

		[Column("inflation_rate")]
		public decimal InflationRate { get; set; }

		[Column("tax_rate")]
		public decimal TaxRate { get; set; }

		[Column("created_at")]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		[Column("total_contributions")]
		public decimal TotalContributions { get; set; }

		[Column("total_interest_before_tax")]
		public decimal TotalInterestBeforeTax { get; set; }

		[Column("total_interest_after_tax")]
		public decimal TotalInterestAfterTax { get; set; }

		[Column("final_balance")]
		public decimal FinalBalance { get; set; }

		[Column("inflation_adjusted_balance")]
		public decimal InflationAdjustedBalance { get; set; }
	}
}
