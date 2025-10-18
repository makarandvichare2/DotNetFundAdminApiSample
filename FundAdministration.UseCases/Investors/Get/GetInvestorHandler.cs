using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.Common.Investors;
using FundAdministration.Infrastructure.Data.Queries.Funds;

namespace FundAdministration.UseCases.Investors.Get;

public class GetInvestorHandler(IGetInvestorQueryService _query)
  : IQueryHandler<GetInvestorQuery, Result<CreateInvestorDataDTO>>
{
  public async Task<Result<CreateInvestorDataDTO>> Handle(GetInvestorQuery request, CancellationToken cancellationToken)
  {
    var result = await _query.InvestorDataAsync(request.guid);

    return Result.Success(result);
  }
}
