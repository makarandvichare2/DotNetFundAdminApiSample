using FundAdministration.Common.Investors;

namespace FundAdministration.Infrastructure.Data.Queries.Funds;

public interface IListInvestorQueryService
{
  Task<IEnumerable<InvestorListDTO>> ListAsync();
}
