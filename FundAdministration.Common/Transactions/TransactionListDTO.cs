using FundAdministration.Common.Enum;

namespace FundAdministration.Common.Transactions
{
    public record TransactionListDTO(
    decimal amount,
    DateTime transactionDate,
    TransactionType transactionType);
}
