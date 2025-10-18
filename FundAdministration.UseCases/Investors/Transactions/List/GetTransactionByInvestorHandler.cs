using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.DTOs.Investors;
using FundAdministration.Infrastructure.Data.Queries.Investors;

namespace FundAdministration.UseCases.Investors.List;

public class GetTransactionByInvestorHandler(IListInvestorQueryService _query)
  : IQueryHandler<ListInvestorQuery, Result<IEnumerable<InvestorListDTO>>>
{
  public async Task<Result<IEnumerable<InvestorListDTO>>> Handle(ListInvestorQuery request, CancellationToken cancellationToken)
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
