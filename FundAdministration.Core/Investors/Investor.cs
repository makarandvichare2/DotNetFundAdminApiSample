using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using FundAdministration.Core.Base;
using FundAdministration.Core.Funds;
using FundAdministration.Core.Transactions;

namespace FundAdministration.Core.Investors;

public class Investor : SoftDeletableEntityBase, IAggregateRoot
{
    public Investor() { }
    public Investor(string fullName,
    Email email,
    Guid fundId
    )
    {
        Guard.Against.NullOrWhiteSpace(fullName, nameof(fullName));
        Guard.Against.NullOrWhiteSpace(email.EmailId, nameof(Email.EmailId));
        Guard.Against.Default(fundId, nameof(fundId));
        FullName = fullName;
        Email = email;
        FundId = fundId;
    }
    public string FullName { get; private set; }
    public Email Email { get; private set; }
    public Guid FundId { get; private set; }

    public Fund Fund { get; set; }

    public ICollection<Transaction> Transactions { get; set; }

    public void UpdateEmail(string emailId) => Email = new Email(emailId);

    public void UpdateFullName(string newFullName)
    {
        Guard.Against.NullOrWhiteSpace(newFullName, nameof(newFullName));
        FullName = newFullName;
    }
    public void UpdateFundId(Guid newFundId)
    {
        Guard.Against.Default(newFundId, nameof(newFundId));
        FundId = newFundId;
    }
}
