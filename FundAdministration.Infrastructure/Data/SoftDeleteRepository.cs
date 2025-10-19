using Ardalis.SharedKernel;
using Ardalis.Specification.EntityFrameworkCore;
using FundAdministration.Core.Base;

namespace FundAdministration.Infrastructure.Data;
public class SoftDeleteRepository<T>(AppDbContext dbContext) : RepositoryBase<T>(dbContext), ISoftDeleteRepository<T> 
    where T : SoftDeletableEntityBase, IAggregateRoot
{

    //public virtual async Task<T?> GetByGuidAsync(Guid guid, CancellationToken cancellationToken = default)
    //{
    //    return await dbContext.Set<T>().FirstOrDefaultAsync(o=>o.Id == guid, cancellationToken: cancellationToken);
    //}
}
