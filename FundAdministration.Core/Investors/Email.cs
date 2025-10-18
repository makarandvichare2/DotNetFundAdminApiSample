using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Core.Investors
{
    public class Email : ValueObject
    {
        public Email(string emailId)
        {
            Guard.Against.NullOrWhiteSpace(emailId, nameof(emailId));
            // ToDo: Proper Email Validation
            EmailId= emailId;
        }
        public string EmailId { get; private set; } 
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EmailId;
        }
    }
}
