using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using FundAdministration.Core.Base;
using FundAdministration.Core.Transactions.Enum;

namespace FundAdministration.Core.Transactions;

public class Transaction : ApiEntityBase, IAggregateRoot
{
    public Transaction() { }
    public Transaction(int investorId,
    TransactionType transactionType,
    decimal amount,
    DateTime transactionDate
    )
    {
        Guard.Against.NegativeOrZero(investorId, nameof(investorId));
        Guard.Against.Negative(amount, nameof(amount));
        Guard.Against.Default(transactionDate, nameof(transactionDate));

        InvestorId = investorId;
        TransactionType = transactionType;
        Amount = amount;
    }
    public int InvestorId { get; private set; }
    public TransactionType TransactionType { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime TransactionDate { get; private set; }
}
