using FundAdministration.DTOs.Investors;

namespace FundAdministration.Infrastructure.Data.Queries.Investors;

public interface IGetInvestorQueryService
{
  Task<CreateInvestorDataDTO> InvestorDataAsync(Guid guid);
}
