namespace FundAdministration.API.Controllers.Investors
{
    public record UpdateInvestorRequest(
        Guid id,
        string fullName,
        string emailId,
        Guid fundId);
}
