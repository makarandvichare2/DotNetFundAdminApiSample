using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using FundAdministration.Common.Enum;
using FundAdministration.Core.Base;
using FundAdministration.Core.Investors;

namespace FundAdministration.Core.Transactions;

public class Transaction : ApiEntityBase, IAggregateRoot
{
    public Transaction() { }
    public Transaction(Guid investorId,
    TransactionType transactionType,
    decimal amount,
    DateTime transactionDate
    )
    {
        Guard.Against.Default(investorId, nameof(investorId));
        Guard.Against.Negative(amount, nameof(amount));
        Guard.Against.Default(transactionDate, nameof(transactionDate));

        InvestorId = investorId;
        TransactionType = transactionType;
        Amount = amount;
        TransactionDate = transactionDate;
    }
    public Guid InvestorId { get; private set; }
    public TransactionType TransactionType { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime TransactionDate { get; private set; }

    public Investor Investor { get; set; }

}
