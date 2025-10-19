using FundAdministration.Common.Reports;
using Microsoft.EntityFrameworkCore;

namespace FundAdministration.Infrastructure.Data.Queries.Reports;

public class NetInvestmentPerFundQueryService(AppDbContext _db) : INetInvestmentPerFundQueryService
{
    public async Task<IEnumerable<NetInvestmentPerFundListDTO>> ListAsync()
    {
        var sql = @"WITH CTEQuery AS (
                                        SELECT 
                                            f.Id AS FundId,
                                            f.FundName,
                                            t.TransactionType,
                                            t.Amount
                                        FROM Transactions t
                                        INNER JOIN Investors i ON t.InvestorId = i.Id
                                        INNER JOIN Funds f ON i.FundId = f.Id
                                    )
                    SELECT 
                        FundName,
                        SUM(CASE 
                                WHEN TransactionType = 0 THEN Amount 
                                WHEN TransactionType = 1 THEN -Amount 
                            END) AS NetAmount
                    FROM CTEQuery
                    GROUP BY FundName
                ";
        var result = await _db.Database.SqlQueryRaw<NetInvestmentPerFundListDTO>(sql).ToListAsync();

        return result;
    }
}
