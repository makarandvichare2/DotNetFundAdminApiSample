using FundAdministration.DTOs.Funds;

namespace FundAdministration.Infrastructure.Data.Queries.Funds;

public interface IListFundQueryService
{
  Task<IEnumerable<FundListDTO>> ListAsync();
}
