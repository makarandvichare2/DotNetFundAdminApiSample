using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.Common.Enum;

namespace FundAdministration.UseCases.Transactions.Register;
public record RegisterTransactionCommand
        (
            TransactionType transactionType,
            decimal amount,
            DateTime transactionDate,
            int investorId
         ) : ICommand<Result<Guid>>;

