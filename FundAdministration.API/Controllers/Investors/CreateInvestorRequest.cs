namespace FundAdministration.API.Controllers.Investors
{
    public record CreateInvestorRequest(
        string fullName,
        string emailId,
        int fundId);
}
