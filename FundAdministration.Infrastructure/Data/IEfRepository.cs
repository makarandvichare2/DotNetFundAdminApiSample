using Ardalis.SharedKernel;
using FundAdministration.Core.Base;

namespace FundAdministration.Infrastructure.Data
{
    public interface IEfRepository<T> : IRepository<T> where T : ApiEntityBase, IAggregateRoot
    {
        Task<T?> GetByGuidAsync(Guid guid, CancellationToken cancellationToken = default);
    }
}
