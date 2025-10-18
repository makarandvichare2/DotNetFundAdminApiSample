using Ardalis.Result;
using Ardalis.SharedKernel;

namespace FundAdministration.UseCases.Funds.List;

public class ListFundHandler(IListFundQueryService _query)
  : IQueryHandler<ListFundQuery, Result<IEnumerable<FundListDTO>>>
{
  public async Task<Result<IEnumerable<FundListDTO>>> Handle(ListFundQuery request, CancellationToken cancellationToken)
  {
    var result = await _query.ListAsync();

    return Result.Success(result);
  }
}
