using Ardalis.SharedKernel;

namespace FundAdministration.Infrastructure.Data;

public interface INonDeletableRepository<T> : IRepository<T>, IReadRepository<T> where T : EntityBase<Guid>, IAggregateRoot
{
}
