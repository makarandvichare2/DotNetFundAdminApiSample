using Ardalis.Result;
using Ardalis.SharedKernel;

namespace FundAdministration.UseCases.Investors.Get;

public record GetInvestorQuery(Guid guid) : IQuery<Result<CreateInvestorDataDTO>>;
