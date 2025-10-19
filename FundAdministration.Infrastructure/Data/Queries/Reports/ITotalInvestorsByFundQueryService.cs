using FundAdministration.Common.Reports;

namespace FundAdministration.Infrastructure.Data.Queries.Reports;

public interface ITotalInvestorsByFundQueryService
{
  Task<IEnumerable<TotalInvestorsByFundListDTO>> ListAsync();
}
