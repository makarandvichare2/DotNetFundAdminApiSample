using Ardalis.GuardClauses;

namespace FundAdministration.Core.Base
{
    public abstract class ApiEntityBase : Ardalis.SharedKernel.EntityBase<Guid>
    {
        public bool IsDeleted { get; protected set; }

        protected ApiEntityBase()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }

        protected ApiEntityBase(Guid guId)
        {
            Guard.Against.Default(guId, nameof(guId));
            Id = guId;
            IsDeleted = false;
        }

        public void UpdateIsDeleted(bool isDeleted) => IsDeleted = isDeleted;
    }
}
