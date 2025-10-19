using FundAdministration.Common.Reports;
using Microsoft.EntityFrameworkCore;

namespace FundAdministration.Infrastructure.Data.Queries.Reports;

public class TotalInvestorsByFundQueryService(AppDbContext _db) : ITotalInvestorsByFundQueryService
{
    public async Task<IEnumerable<TotalInvestorsByFundListDTO>> ListAsync()
    {
        var sql = @"SELECT 
	                    fundName,
                        COUNT(i.Id) totalInvestors
                    FROM Investors i 
                    JOIN Funds f ON i.fundId = f.id
                    GROUP BY FundName
                ";
        var result = await _db.Database.SqlQueryRaw<TotalInvestorsByFundListDTO>(sql).ToListAsync();

        return result;
    }
}
