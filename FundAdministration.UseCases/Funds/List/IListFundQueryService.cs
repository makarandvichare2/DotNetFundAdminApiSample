namespace FundAdministration.UseCases.Funds.List;

public interface IListFundQueryService
{
  Task<IEnumerable<FundListDTO>> ListAsync();
}
