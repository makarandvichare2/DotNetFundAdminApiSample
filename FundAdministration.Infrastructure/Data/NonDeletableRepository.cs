using Ardalis.SharedKernel;
using Ardalis.Specification.EntityFrameworkCore;

namespace FundAdministration.Infrastructure.Data;
public class NonDeletableRepository<T>(AppDbContext dbContext) : RepositoryBase<T>(dbContext), IReadRepository<T> , INonDeletableRepository<T>
    where T : EntityBase<Guid>, IAggregateRoot
{
}
