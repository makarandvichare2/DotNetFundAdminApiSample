namespace FundAdministration.API.Controllers.Investors
{
    public record RegisterTransactionRequest(
        string fullName,
        string emailId,
        int fundId);
}
