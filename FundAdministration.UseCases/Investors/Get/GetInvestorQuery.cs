using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.DTOs.Investors;

namespace FundAdministration.UseCases.Investors.Get;

public record GetInvestorQuery(Guid guid) : IQuery<Result<CreateInvestorDataDTO>>;
