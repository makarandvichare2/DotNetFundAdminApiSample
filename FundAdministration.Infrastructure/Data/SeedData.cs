using FundAdministration.Core.Funds;
using FundAdministration.Core.Investors;
using FundAdministration.Core.Transactions;
using FundAdministration.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FundAdministration.Infrastructure.Data;

public static class SeedData
{
    public static async Task InitializeAsync(AppDbContext dbContext)
    {
        if (await dbContext.Funds.AnyAsync()) return;

        await PopulateFundDataAsync(dbContext);
        await PopulateInvestorDataAsync(dbContext);
        await PopulateTransactionDataAsync(dbContext);
    }

    public static async Task PopulateFundDataAsync(AppDbContext dbContext)
    {
        List<dynamic> fundsDto = await GetSampleData("funds.json");

        var funds = fundsDto.Select(f => new Fund(
                                        f.FundName,
                                        new Currency(f.CurrencyCode),
                                        f.LaunchDate
                                    )).ToList();

        await dbContext.Funds.AddRangeAsync(funds);
        await dbContext.SaveChangesAsync();
    }

    public static async Task PopulateInvestorDataAsync(AppDbContext dbContext)
    {
        List<dynamic> dtos = await GetSampleData("investors.json");

        var fundLookup = dbContext.Funds
            .GroupBy(f => f.FundName)
            .ToDictionary(k => k.Key, v=>v.First().Id);

        var investors = dtos.Select(o => new Investor(
            o.fullName, 
            new Email(o.emailId),
            fundLookup[o.fundName])
            ).ToList();

        await dbContext.Investors.AddRangeAsync(investors);
        await dbContext.SaveChangesAsync();
    }

    private static async Task<List<dynamic>> GetSampleData(string fileName)
    {
        var filePath = FileHelpers.GetSampleDataFilePath(fileName);
        var json = await File.ReadAllTextAsync(filePath);
        var dtos = JsonSerializer.Deserialize<List<dynamic>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new List<dynamic>();
        return dtos;
    }

    public static async Task PopulateTransactionDataAsync(AppDbContext dbContext)
    {
        List<dynamic> dtos = await GetSampleData("transactions.json");

        var investorLookup = dbContext.Investors
            .GroupBy(f => f.FullName)
            .ToDictionary(k => k.Key, v => v.First().Id);

        var transactions = dtos.Select(o => new Transaction(
                                        investorLookup[o.fundName],
                                        o.transactionType,
                                        o.amount,
                                        o.transactionDate
                                    )).ToList();

        await dbContext.Transactions.AddRangeAsync(transactions);
        await dbContext.SaveChangesAsync();
    }

}