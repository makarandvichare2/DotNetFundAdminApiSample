using FundAdministration.DTOs.Investors;
using Microsoft.EntityFrameworkCore;

namespace FundAdministration.Infrastructure.Data.Queries.Investors;

public class ListInvestorQueryService(AppDbContext _db) : IListInvestorQueryService
{
    public async Task<IEnumerable<InvestorListDTO>> ListAsync()
    {
        var result = await _db.Database.SqlQuery<InvestorListDTO>(
          @$"SELECT 
                    i.Guid,
                    FullName,
                    EmailId,
                    FundName
             FROM Investors i Join Funds f
             ON i.FundId = f.Id
             Order by FullName
        ")
          .ToListAsync();

        return result;
    }
}
