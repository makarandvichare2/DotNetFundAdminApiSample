using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.Common.Funds;

namespace FundAdministration.UseCases.Funds.Get;

public record GetFundQuery(Guid guid) : IQuery<Result<CreateFundDataDTO>>;
