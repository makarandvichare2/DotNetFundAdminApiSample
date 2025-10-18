using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.Common.Investors;
using FundAdministration.Infrastructure.Data.Queries.Funds;

namespace FundAdministration.UseCases.Investors.List;

public class ListInvestorHandler(IListInvestorQueryService _query)
  : IQueryHandler<ListInvestorQuery, Result<IEnumerable<InvestorListDTO>>>
{
  public async Task<Result<IEnumerable<InvestorListDTO>>> Handle(ListInvestorQuery request, CancellationToken cancellationToken)
  {
    var result = await _query.ListAsync();

    return Result.Success(result);
  }
}
