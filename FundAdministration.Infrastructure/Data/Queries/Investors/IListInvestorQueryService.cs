using FundAdministration.DTOs.Investors;

namespace FundAdministration.Infrastructure.Data.Queries.Investors;

public interface IListInvestorQueryService
{
  Task<IEnumerable<InvestorListDTO>> ListAsync();
}
