using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.Common.Investors;

namespace FundAdministration.UseCases.Investors.Get;

public record GetInvestorQuery(Guid id) : IQuery<Result<CreateInvestorDataDTO>>;
