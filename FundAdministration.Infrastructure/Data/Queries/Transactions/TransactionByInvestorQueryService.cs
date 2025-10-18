using FundAdministration.Common.Transactions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FundAdministration.Infrastructure.Data.Queries.Transactions;

public class TransactionByInvestorQueryService(AppDbContext _db) : ITransactionByInvestorQueryService
{
    public async Task<IEnumerable<TransactionListDTO>> ListAsync(Guid investorId)
    {
        var investorIddParam = new SqlParameter("@InvestorId", investorId);
        var result = await _db.Database.SqlQueryRaw<TransactionListDTO>(
                  @"SELECT 
                            transactionType,
                            transactionDate,
                            amount
                     FROM Transactions t 
                     Where investorId = @InvestorId
                     Order by transactionDate
                ", investorIddParam)
          .ToListAsync();

        return result;
    }
}
