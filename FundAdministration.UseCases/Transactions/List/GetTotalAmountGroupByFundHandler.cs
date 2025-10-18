using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.Common.Transactions;
using FundAdministration.Infrastructure.Data.Queries.Transactions;
namespace FundAdministration.UseCases.Transactions.List;

public class GetTotalAmountGroupByFundHandler(ITotalAmountGroupByFundQueryService _query)
  : IQueryHandler<GetTotalAmountGroupByFundQuery, Result<IEnumerable<GroupAmountByFundListDTO>>>
{
  public async Task<Result<IEnumerable<GroupAmountByFundListDTO>>> Handle(GetTotalAmountGroupByFundQuery request, CancellationToken cancellationToken)
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
