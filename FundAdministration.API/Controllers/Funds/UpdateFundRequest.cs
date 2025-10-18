namespace FundAdministration.API.Controllers.Funds
{
    public record UpdateFundRequest(
        Guid guid,
        string fundName,
        string currencyCode,
        DateTime launchDate);
}
