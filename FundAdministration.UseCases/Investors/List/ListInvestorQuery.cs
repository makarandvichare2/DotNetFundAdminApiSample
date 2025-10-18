using Ardalis.Result;
using Ardalis.SharedKernel;

namespace FundAdministration.UseCases.Investors.List;

public record ListInvestorQuery() : IQuery<Result<IEnumerable<InvestorListDTO>>>;
