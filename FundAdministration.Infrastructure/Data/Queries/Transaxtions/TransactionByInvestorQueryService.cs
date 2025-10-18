using FundAdministration.Common.Investors;
using FundAdministration.Common.Transactions;
using Microsoft.EntityFrameworkCore;

namespace FundAdministration.Infrastructure.Data.Queries.Investors;

public class TransactionByInvestorQueryService(AppDbContext _db) : ITransactionByInvestorQueryService
{
    public async Task<IEnumerable<TransactionListDTO>> ListAsync()
    {
        var result = await _db.Database.SqlQuery<TransactionListDTO>(
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
