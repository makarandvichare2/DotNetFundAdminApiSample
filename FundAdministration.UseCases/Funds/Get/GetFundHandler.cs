using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.Common.Funds;
using FundAdministration.Infrastructure.Data.Queries.Funds;

namespace FundAdministration.UseCases.Funds.Get;

public class GetFundHandler(IGetFundQueryService _query)
  : IQueryHandler<GetFundQuery, Result<CreateFundDataDTO>>
{
  public async Task<Result<CreateFundDataDTO>> Handle(GetFundQuery request, CancellationToken cancellationToken)
  {
        try
        {
            var result = await _query.FundDataAsync(request.id);
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            // _logger.LogError(ex, "Error creating fund");
            return Result.Error(ex.Message);
        }
    }
}
