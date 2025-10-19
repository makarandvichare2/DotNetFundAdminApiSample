using FundAdministration.Common.Reports;

namespace FundAdministration.Infrastructure.Data.Queries.Reports;

public interface INetInvestmentPerFundQueryService
{
  Task<IEnumerable<NetInvestmentPerFundListDTO>> ListAsync();
}
