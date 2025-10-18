using FundAdministration.Common.Enum;

namespace FundAdministration.API.Controllers.Investors
{
    public record RegisterTransactionRequest(
        TransactionType transactionType,
        decimal amount,
        DateTime transactionDate,
        Guid investorId);
}
