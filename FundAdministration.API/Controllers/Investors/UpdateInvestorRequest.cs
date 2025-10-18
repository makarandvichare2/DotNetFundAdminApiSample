namespace FundAdministration.API.Controllers.Investors
{
    public record UpdateInvestorRequest(
        Guid guid,
        string fullName,
        string emailId,
        Guid fundId);
}
