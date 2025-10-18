using FundAdministration.UseCases.Funds.Get;
using Microsoft.EntityFrameworkCore;

namespace FundAdministration.Infrastructure.Data.Queries.Funds;

public class GetFundQueryService(AppDbContext _db) : IGetFundQueryService
{
    public async Task<CreateFundDataDTO> FundDataAsync(Guid guid)
    {
        var currencies = new List<string>
            {
                "EUR",
                "USD",
                "GBP"
            };
        var fund = await _db.Funds.AsNoTracking().FirstOrDefaultAsync(o=>o.GuId == guid);


        var lauchDate = (fund!= null) ? fund.LaunchDate : DateTime.Now;
        var fundName = fund?.FundName ?? string.Empty;
        var currencyCode = fund?.Currency?.CurrencyCode ?? currencies.FirstOrDefault();

        var fundDto = new CreateFundDTO(fundName,
                                    currencyCode,
                                    lauchDate);

        var createFundDto = new CreateFundDataDTO(fundDto, currencies);
        return createFundDto;
    }
}
