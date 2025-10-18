using FundAdministration.UseCases.Funds.List;
using FundAdministration.UseCases.Investors.List;
using Microsoft.EntityFrameworkCore;

namespace FundAdministration.Infrastructure.Data.Queries.Funds;

public class ListInvestorQueryService(AppDbContext _db) : IListInvestorQueryService
{
    public async Task<IEnumerable<InvestorListDTO>> ListAsync()
    {
        var result = await _db.Database.SqlQuery<InvestorListDTO>(
          @$"SELECT 
                    FundName,
                    CurrencyCode,
                    LaunchDate
             FROM Funds 
             Order by FundName
        ")
          .ToListAsync();

        return result;
    }
}
