using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using FundAdministration.Core.Base;
using FundAdministration.Core.Investors;

namespace FundAdministration.Core.Funds;

public class Fund : ApiEntityBase, IAggregateRoot
{
    public Fund() { }
    public Fund(string fundName,
    Currency currency,
    DateTime launchDate
    )
    {
        Guard.Against.NullOrWhiteSpace(fundName, nameof(fundName));
        Guard.Against.NullOrWhiteSpace(currency.CurrencyCode, nameof(Currency.CurrencyCode));
        Guard.Against.Default(launchDate, nameof(launchDate));
        FundName = fundName;
        Currency = currency;
        LaunchDate = launchDate;
    }
    public string FundName { get; private set; }
    public Currency Currency { get; private set; }
    public DateTime LaunchDate { get; private set; }

    public ICollection<Investor> Investors { get; set; }

    public void UpdateCurrency(string currencyCode) => Currency = new Currency(currencyCode);

    public void UpdateFundName(string newFundName)
    {
        Guard.Against.NullOrWhiteSpace(newFundName, nameof(newFundName));
        FundName = newFundName;
    }

    public void UpdateLaunchDate(DateTime newLaunchDate)
    {
        Guard.Against.Default(newLaunchDate, nameof(newLaunchDate));
        LaunchDate = newLaunchDate;
    }
}
