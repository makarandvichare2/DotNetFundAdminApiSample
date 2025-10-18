using FundAdministration.Common.Enum;
using FundAdministration.Common.Transactions;
using Microsoft.EntityFrameworkCore;

namespace FundAdministration.Infrastructure.Data.Queries.Transactions;

public class TotalAmountGroupByFundQueryService(AppDbContext _db) : ITotalAmountGroupByFundQueryService
{
    public async Task<IEnumerable<GroupAmountByFundListDTO>> ListAsync()
    {
        var sql = @"SELECT 
	                    fundName,
                        TransactionType as transactionTypeId,
                        '' as transactionType,
                        Sum(amount) totalAmount
                    FROM Transactions t Join Investors i
		                    ON t.investorId = i.id
	                    Join Funds f ON i.fundId = f.id
                    Group by FundName,transactionType
                ";
        var result = await _db.Database.SqlQueryRaw<GroupAmountByFundListDTO>(sql).ToListAsync();

        result.ForEach(o => o.transactionType = GetTransactionType(o.transactionTypeId));
        return result;
    }

    private string GetTransactionType(TransactionType transactionTypeId)
    {
       return  Enum.GetName(typeof(TransactionType), transactionTypeId);
    }
}
