using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

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
