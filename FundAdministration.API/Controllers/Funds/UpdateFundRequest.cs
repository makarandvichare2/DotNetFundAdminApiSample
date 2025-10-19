namespace FundAdministration.API.Controllers.Funds
{
    public record UpdateFundRequest(
        Guid id,
        string fundName,
        string currencyCode,
        DateTime launchDate);
}
