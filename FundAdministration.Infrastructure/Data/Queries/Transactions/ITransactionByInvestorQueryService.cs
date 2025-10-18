using FundAdministration.Common.Transactions;

namespace FundAdministration.Infrastructure.Data.Queries.Transactions;

public interface ITransactionByInvestorQueryService
{
  Task<IEnumerable<TransactionListDTO>> ListAsync(Guid guid);
}
