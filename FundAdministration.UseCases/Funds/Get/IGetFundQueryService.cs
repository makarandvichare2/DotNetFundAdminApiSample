namespace FundAdministration.UseCases.Funds.Get;

public interface IGetFundQueryService
{
  Task<CreateFundDataDTO> FundDataAsync();
}
