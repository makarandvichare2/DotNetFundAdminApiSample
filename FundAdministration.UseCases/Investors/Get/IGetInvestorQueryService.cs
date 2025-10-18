namespace FundAdministration.UseCases.Investors.Get;

public interface IGetInvestorQueryService
{
  Task<CreateInvestorDataDTO> InvestorDataAsync();
}
