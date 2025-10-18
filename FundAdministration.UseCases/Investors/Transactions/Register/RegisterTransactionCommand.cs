using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.Core.Transactions.Enum;

namespace FundAdministration.UseCases.Investors.Create;
public record RegisterTransactionCommand
        (
            TransactionType transactionType,
            decimal amount,
            DateTime transactionDate,
            int investorId
         ) : ICommand<Result<Guid>>;

