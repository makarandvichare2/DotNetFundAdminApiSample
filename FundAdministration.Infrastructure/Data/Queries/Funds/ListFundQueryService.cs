using FundAdministration.Common.Funds;
using Microsoft.EntityFrameworkCore;

namespace FundAdministration.Infrastructure.Data.Queries.Funds;

public class ListFundQueryService(AppDbContext _db) : IListFundQueryService
{
    public async Task<IEnumerable<FundListDTO>> ListAsync()
    {
        var result = await _db.Database.SqlQuery<FundListDTO>(
          @$"SELECT 
                    Guid,
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
