using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using FundAdministration.Core.Base;
using FundAdministration.Core.Funds;

namespace FundAdministration.Core.Investors;

public class Investor : ApiEntityBase, IAggregateRoot
{
    public Investor() { }
    public Investor(string fullName,
    Email email,
    int fundId
    )
    {
        Guard.Against.NullOrWhiteSpace(fullName, nameof(fullName));
        Guard.Against.NullOrWhiteSpace(email.EmailId, nameof(Email.EmailId));
        Guard.Against.NegativeOrZero(fundId, nameof(fundId));
        FullName = fullName;
        Email = email;
        FundId = fundId;
    }
    public string FullName { get; private set; }
    public Email Email { get; private set; }
    public int FundId { get; private set; }
    public void UpdateEmail(string emailId) => Email = new Email(emailId);

    public void UpdateFullName(string newFullName)
    {
        Guard.Against.NullOrWhiteSpace(newFullName, nameof(newFullName));
        FullName = newFullName;
    }
    public void UpdateFundId(int newFundId)
    {
        Guard.Against.NegativeOrZero(newFundId, nameof(newFundId));
        FundId = newFundId;
    }
}
