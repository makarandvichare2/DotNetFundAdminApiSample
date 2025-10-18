using FundAdministration.Common.Funds;

namespace FundAdministration.Infrastructure.Data.Queries.Funds;

public interface IGetFundQueryService
{
  Task<CreateFundDataDTO> FundDataAsync(Guid guid);
}
