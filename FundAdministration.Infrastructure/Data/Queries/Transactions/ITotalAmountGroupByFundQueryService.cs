using FundAdministration.Common.Transactions;

namespace FundAdministration.Infrastructure.Data.Queries.Transactions;

public interface ITotalAmountGroupByFundQueryService
{
  Task<IEnumerable<GroupAmountByFundListDTO>> ListAsync();
}
