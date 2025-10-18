using FundAdministration.UseCases.Common;
using FundAdministration.UseCases.Funds.Get;
using FundAdministration.UseCases.Investors.Get;
using Microsoft.EntityFrameworkCore;

namespace FundAdministration.Infrastructure.Data.Queries.Funds;

public class GetInvestorQueryService(AppDbContext _db) : IGetInvestorQueryService
{
    public async Task<CreateInvestorDataDTO> InvestorDataAsync()
    {

        var funds = await _db.Database.SqlQuery<LookUpDTO<int>>(
      $"SELECT Id, Name FROM Funds Order by FundName").ToListAsync();
        var fund = new CreateInvestorDTO(string.Empty,
                                         string.Empty,
                                        funds.FirstOrDefault().Id);

        var createFundDto = new CreateInvestorDataDTO(fund, funds);
        return createFundDto;
    }
}
