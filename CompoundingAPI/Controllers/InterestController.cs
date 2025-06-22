using CompoundingAPI.Data;
using CompoundingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

[ApiController]
[Route("api/[controller]")]
public class InterestController : ControllerBase
{
    private readonly ILogger<InterestController> _logger;
    private readonly CompoundInterestAppDbContext _context;

    public InterestController(ILogger<InterestController> logger, CompoundInterestAppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("calculate")]
    public ActionResult<CompoundInterestResult> Calculate([FromBody] CompoundInterestRequestDto request)
    {
        _logger.LogInformation("Received compound interest calculation request: {@Request}", request);

        if (request.Years <= 0 || request.CompoundFrequency <= 0)
            return BadRequest("Years and compound frequency must be greater than zero.");

        double initialInvestment = request.InitialInvestment;
        double rate = request.Rate / 100.0;
        double inflationRate = request.InflationRate / 100.0;
        double taxRate = request.TaxRate / 100.0;

        int compoundPerYear = request.CompoundFrequency;
        int totalPeriods = request.Years * compoundPerYear;
        double periodRate = rate / compoundPerYear;

        double balance = initialInvestment;
        double totalContributions = 0;
        double totalInterestEarned = 0;
        double inflationAdjustedBalance = 0;

        double contributionPerPeriod = 0;

        if (request.MakeContributions && request.Contribution > 0)
        {
            if (request.ContributionFrequency == 1) 
                contributionPerPeriod = request.Contribution * (12.0 / compoundPerYear);
            else if (request.ContributionFrequency == 2) 
                contributionPerPeriod = request.Contribution * (4.0 / compoundPerYear);
            else if (request.ContributionFrequency == 3) 
                contributionPerPeriod = request.Contribution;
            else if (request.ContributionFrequency == 4) 
                contributionPerPeriod = request.Contribution * (1.0 / compoundPerYear);
            else
                _logger.LogWarning("Unknown contribution frequency: {Freq}", request.ContributionFrequency);
        }

        for (int i = 1; i <= totalPeriods; i++)
        {
            double interestEarned = balance * periodRate;
            double tax = interestEarned * taxRate;
            double netInterest = interestEarned - tax;

            balance += netInterest;
            totalInterestEarned += netInterest;

            if (request.MakeContributions && contributionPerPeriod > 0)
            {
                balance += contributionPerPeriod;
                totalContributions += contributionPerPeriod;
            }
        }

       
        inflationAdjustedBalance = balance / Math.Pow(1 + (inflationRate / compoundPerYear), totalPeriods);

        var result = new CompoundInterestResult
        {
            TotalYears = request.Years,
            TotalContributions = Math.Round(totalContributions, 2),
            TotalInterestBeforeTax = Math.Round(totalInterestEarned, 2),
            TotalInterestAfterTax = Math.Round(totalInterestEarned, 2),
            FinalBalance = Math.Round(balance, 2),
            InflationAdjustedBalance = Math.Round(inflationAdjustedBalance, 2)
        };

        var record = new CompoundInterestStore
        {
            InitialInvestment = (decimal)request.InitialInvestment,
            Rate = (decimal)request.Rate,
            MakeContributions = request.MakeContributions,
            Contribution = (decimal)request.Contribution,
            ContributionFrequency = request.ContributionFrequency,
            Years = request.Years,
            CompoundFrequency = request.CompoundFrequency,
            InflationRate = (decimal)request.InflationRate,
            TaxRate = (decimal)request.TaxRate,
            CreatedAt = DateTime.UtcNow,

            TotalContributions = (decimal)totalContributions,
            TotalInterestBeforeTax = (decimal)totalInterestEarned,
            TotalInterestAfterTax = (decimal)totalInterestEarned,
            FinalBalance = (decimal)balance,
            InflationAdjustedBalance = (decimal)inflationAdjustedBalance
        };

        _context.Records.Add(record);
        _context.SaveChanges();

        _logger.LogInformation("Calculation complete and record stored: {@Record}", record);

        return Ok(result);
    }
}