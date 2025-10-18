using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.Common.Transactions;

namespace FundAdministration.UseCases.Transactions.List;

public record GetTotalAmountGroupByFundQuery() : IQuery<Result<IEnumerable<GroupAmountByFundListDTO>>>;
