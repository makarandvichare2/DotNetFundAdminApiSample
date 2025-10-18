using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.Infrastructure.Data.Queries.Investors;
using FundAdministration.Common.Transactions;
namespace FundAdministration.UseCases.Transactions.List;

public class GetTransactionByInvestorHandler(ITransactionByInvestorQueryService _query)
  : IQueryHandler<GetTransactionByInvestorQuery, Result<IEnumerable<TransactionListDTO>>>
{
  public async Task<Result<IEnumerable<TransactionListDTO>>> Handle(GetTransactionByInvestorQuery request, CancellationToken cancellationToken)
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
