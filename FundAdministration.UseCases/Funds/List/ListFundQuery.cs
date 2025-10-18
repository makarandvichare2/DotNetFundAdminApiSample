using Ardalis.Result;
using Ardalis.SharedKernel;

namespace FundAdministration.UseCases.Funds.List;

public record ListFundQuery() : IQuery<Result<IEnumerable<FundListDTO>>>;
