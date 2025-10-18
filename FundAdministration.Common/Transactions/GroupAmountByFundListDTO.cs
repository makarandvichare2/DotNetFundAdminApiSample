using FundAdministration.Common.Enum;

namespace FundAdministration.Common.Transactions
{
    public record GroupAmountByFundListDTO
    {
        public  string fundName { get; init; }
        public  TransactionType transactionTypeId { get; init; }
        public decimal totalAmount { get; init; }
        public string? transactionType { get; set; } = string.Empty;
    }
}
