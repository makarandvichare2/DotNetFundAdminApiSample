using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.Common.Reports;

namespace FundAdministration.UseCases.Reports.List;

public record GetNetInvestmentPerFundQuery() : IQuery<Result<IEnumerable<NetInvestmentPerFundListDTO>>>;
