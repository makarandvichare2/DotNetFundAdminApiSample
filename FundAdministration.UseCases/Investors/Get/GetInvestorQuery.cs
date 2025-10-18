using Ardalis.Result;
using Ardalis.SharedKernel;

namespace FundAdministration.UseCases.Investors.Get;

public record GetInvestorQuery() : IQuery<Result<CreateInvestorDataDTO>>;
