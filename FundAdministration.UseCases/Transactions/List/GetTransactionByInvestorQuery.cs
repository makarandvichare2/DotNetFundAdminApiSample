using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.Common.Transactions;

namespace FundAdministration.UseCases.Transactions.List;

public record GetTransactionByInvestorQuery(Guid guid) : IQuery<Result<IEnumerable<TransactionListDTO>>>;
