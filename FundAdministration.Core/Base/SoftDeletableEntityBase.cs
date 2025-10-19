using Ardalis.GuardClauses;

namespace FundAdministration.Core.Base
{
    public abstract class SoftDeletableEntityBase : Ardalis.SharedKernel.EntityBase<Guid>
    {
        public bool IsDeleted { get; protected set; }

        protected SoftDeletableEntityBase()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }

        protected SoftDeletableEntityBase(Guid guId)
        {
            Guard.Against.Default(guId, nameof(guId));
            Id = guId;
            IsDeleted = false;
        }

        public void UpdateIsDeleted(bool isDeleted) => IsDeleted = isDeleted;
    }
}
