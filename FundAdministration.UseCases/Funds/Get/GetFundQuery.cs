using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.DTOs.Funds;

namespace FundAdministration.UseCases.Funds.Get;

public record GetFundQuery(Guid guid) : IQuery<Result<CreateFundDataDTO>>;
