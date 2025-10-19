using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.Common.Reports;
using FundAdministration.Infrastructure.Data.Queries.Reports;

namespace FundAdministration.UseCases.Reports.List;

public class GetNetInvestmentPerFundQueryHandler(INetInvestmentPerFundQueryService _query)
  : IQueryHandler<GetNetInvestmentPerFundQuery, Result<IEnumerable<NetInvestmentPerFundListDTO>>>
{
  public async Task<Result<IEnumerable<NetInvestmentPerFundListDTO>>> Handle(GetNetInvestmentPerFundQuery request, CancellationToken cancellationToken)
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
