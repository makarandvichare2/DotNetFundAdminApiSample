using FundAdministration.Common.Enum;

namespace FundAdministration.API.Controllers.Transactions
{
    public record RegisterTransactionRequest(
        TransactionType transactionType,
        decimal amount,
        DateTime transactionDate,
        Guid investorId);
}
