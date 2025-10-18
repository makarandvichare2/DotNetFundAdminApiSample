using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.Common.Investors;

namespace FundAdministration.UseCases.Investors.List;

public record ListInvestorQuery() : IQuery<Result<IEnumerable<InvestorListDTO>>>;
