using FundAdministration.UseCases.Funds.Get;

namespace FundAdministration.Infrastructure.Data.Queries.Funds;

public class GetFundQueryService(AppDbContext _db) : IGetFundQueryService
{
    public async Task<CreateFundDataDTO> FundDataAsync()
    {

        var currencies = new List<string>
            {
                "EUR",
                "USD",
                "GBP"
            };
        var fund = new CreateFundDTO(string.Empty,
                                     currencies.FirstOrDefault(),
                                     DateTime.Now);

        var createFundDto = new CreateFundDataDTO(fund, currencies);
        return createFundDto;
    }
}
