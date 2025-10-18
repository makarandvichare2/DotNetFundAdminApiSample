using Ardalis.Result;
using Ardalis.SharedKernel;

namespace FundAdministration.UseCases.Funds.Get;

public class GetFundHandler(IGetFundQueryService _query)
  : IQueryHandler<GetFundQuery, Result<CreateFundDataDTO>>
{
  public async Task<Result<CreateFundDataDTO>> Handle(GetFundQuery request, CancellationToken cancellationToken)
  {
    var result = await _query.FundDataAsync(request.guid);

    return Result.Success(result);
  }
}
