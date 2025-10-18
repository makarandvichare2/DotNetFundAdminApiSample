namespace FundAdministration.API.Controllers.Funds
{
    public record CreateFundRequest(
        string fundName,
        string currencyCode,
        DateTime launchDate);
}
