using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.Common.Funds;

namespace FundAdministration.UseCases.Funds.List;

public record ListFundQuery() : IQuery<Result<IEnumerable<FundListDTO>>>;
