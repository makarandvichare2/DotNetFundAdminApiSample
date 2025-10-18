using FundAdministration.DTOs.Funds;

namespace FundAdministration.Infrastructure.Data.Queries.Funds;

public interface IGetFundQueryService
{
  Task<CreateFundDataDTO> FundDataAsync(Guid guid);
}
