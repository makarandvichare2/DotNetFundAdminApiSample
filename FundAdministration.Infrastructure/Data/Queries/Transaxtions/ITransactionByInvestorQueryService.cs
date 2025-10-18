using FundAdministration.Common.Investors;
using FundAdministration.Common.Transactions;

namespace FundAdministration.Infrastructure.Data.Queries.Investors;

public interface ITransactionByInvestorQueryService
{
  Task<IEnumerable<TransactionListDTO>> ListAsync();
}
