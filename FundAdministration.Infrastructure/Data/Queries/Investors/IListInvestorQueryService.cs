using FundAdministration.Common.Investors;

namespace FundAdministration.Infrastructure.Data.Queries.Investors;

public interface IListInvestorQueryService
{
  Task<IEnumerable<InvestorListDTO>> ListAsync();
}
