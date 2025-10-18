using FundAdministration.UseCases.Investors.List;

namespace FundAdministration.UseCases.Investors.List;

public interface IListInvestorQueryService
{
  Task<IEnumerable<InvestorListDTO>> ListAsync();
}
