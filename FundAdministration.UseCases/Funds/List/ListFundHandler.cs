using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.DTOs.Funds;
using FundAdministration.Infrastructure.Data.Queries.Funds;

namespace FundAdministration.UseCases.Funds.List;

public class ListFundHandler(IListFundQueryService _query)
  : IQueryHandler<ListFundQuery, Result<IEnumerable<FundListDTO>>>
{
  public async Task<Result<IEnumerable<FundListDTO>>> Handle(ListFundQuery request, CancellationToken cancellationToken)
  {
        try
        {
            var result = await _query.ListAsync();
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            // _logger.LogError(ex, "Error creating fund");
            return Result.Error(ex.Message);
        }
    }
}
