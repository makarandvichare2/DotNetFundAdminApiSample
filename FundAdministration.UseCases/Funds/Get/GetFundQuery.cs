using Ardalis.Result;
using Ardalis.SharedKernel;

namespace FundAdministration.UseCases.Funds.Get;

public record GetFundQuery() : IQuery<Result<CreateFundDataDTO>>;
