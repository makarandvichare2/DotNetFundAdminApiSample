using FundAdministration.Common.Investors;

namespace FundAdministration.Infrastructure.Data.Queries.Funds;

public interface IGetInvestorQueryService
{
  Task<CreateInvestorDataDTO> InvestorDataAsync(Guid guid);
}
