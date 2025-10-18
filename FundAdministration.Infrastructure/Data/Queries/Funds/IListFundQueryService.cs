using FundAdministration.Common.Funds;

namespace FundAdministration.Infrastructure.Data.Queries.Funds;

public interface IListFundQueryService
{
  Task<IEnumerable<FundListDTO>> ListAsync();
}
