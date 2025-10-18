using Ardalis.SharedKernel;
using Ardalis.Specification.EntityFrameworkCore;
using FundAdministration.Core.Base;
using Microsoft.EntityFrameworkCore;

namespace FundAdministration.Infrastructure.Data;
public class EfRepository<T>(AppDbContext dbContext) :
  RepositoryBase<T>(dbContext), IReadRepository<T>, IEfRepository<T> where T : ApiEntityBase, IAggregateRoot
{
    public virtual async Task<T?> GetByGuidAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>().FirstOrDefaultAsync(o=>o.Id == guid, cancellationToken: cancellationToken);
    }
}
