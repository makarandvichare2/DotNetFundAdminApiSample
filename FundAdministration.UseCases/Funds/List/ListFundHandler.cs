using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.Common.Funds;
using FundAdministration.Infrastructure.Data.Queries.Funds;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Caching;

namespace FundAdministration.UseCases.Funds.List;

public class ListFundHandler(IListFundQueryService _query, IAsyncCacheProvider _cacheProvider)
  : IQueryHandler<ListFundQuery, Result<IEnumerable<FundListDTO>>>
{
    private const string AllFundsCacheKey = "AllFundsList";

    public async Task<Result<IEnumerable<FundListDTO>>> Handle(ListFundQuery request, CancellationToken cancellationToken)
  {
        try
        {
            var cachingPolicy = Policy.CacheAsync(_cacheProvider, TimeSpan.FromMinutes(5));

            var result = await cachingPolicy.ExecuteAsync(async (ctx, token) =>
            {
                // This delegate only runs if the cache misses! (The slow path)
                var funds = await _query.ListAsync();

                return Result.Success(funds);

            }, new Context(AllFundsCacheKey), cancellationToken); // Pass the cache key here

            return result;
        }
        catch (Exception ex)
        {
            // _logger.LogError(ex, "Error creating fund");
            return Result.Error(ex.Message);
        }
    }
}
