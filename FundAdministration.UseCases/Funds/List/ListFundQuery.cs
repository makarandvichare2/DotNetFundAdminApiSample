using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.DTOs.Funds;

namespace FundAdministration.UseCases.Funds.List;

public record ListFundQuery() : IQuery<Result<IEnumerable<FundListDTO>>>;
