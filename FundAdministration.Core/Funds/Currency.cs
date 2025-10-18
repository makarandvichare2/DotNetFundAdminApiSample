using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace FundAdministration.Core.Funds
{
    public class Currency : ValueObject
    {
        public Currency(string currencyCode) {
            Guard.Against.NullOrWhiteSpace(currencyCode, nameof(currencyCode));
            Guard.Against.LengthOutOfRange(currencyCode, 3, 3, nameof(currencyCode));
            CurrencyCode = currencyCode;
        }
        public string CurrencyCode { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CurrencyCode;
        }
    }
}
