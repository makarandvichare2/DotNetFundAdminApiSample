using Ardalis.SharedKernel;
using FundAdministration.Core.Base;

namespace FundAdministration.Infrastructure.Data;
public interface ISoftDeleteRepository<T> : IRepository<T> , IReadRepository<T> where T : SoftDeletableEntityBase, IAggregateRoot
{
}
