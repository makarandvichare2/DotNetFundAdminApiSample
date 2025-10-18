using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using FundAdministration.Core.Funds;

namespace FundAdministration.Core.Base
{
    public abstract class ApiEntityBase : Ardalis.SharedKernel.EntityBase
    {
        public Guid GuId { get; protected set; }
        public bool IsDeleted { get; protected set; }

        protected ApiEntityBase()
        {
            GuId = Guid.NewGuid();
            IsDeleted = false;
        }

        protected ApiEntityBase(Guid guId)
        {
            Guard.Against.Default(guId, nameof(guId));
            GuId = guId;
            IsDeleted = false;
        }

        public void UpdateIsDeleted(bool isDeleted) => IsDeleted = isDeleted;
    }
}
