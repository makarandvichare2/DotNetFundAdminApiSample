using Ardalis.Result;
using Ardalis.SharedKernel;

namespace FundAdministration.UseCases.Investors.Get;

public class GetInvestorHandler(IGetInvestorQueryService _query)
  : IQueryHandler<GetInvestorQuery, Result<CreateInvestorDataDTO>>
{
  public async Task<Result<CreateInvestorDataDTO>> Handle(GetInvestorQuery request, CancellationToken cancellationToken)
  {
    var result = await _query.InvestorDataAsync();

    return Result.Success(result);
  }
}
