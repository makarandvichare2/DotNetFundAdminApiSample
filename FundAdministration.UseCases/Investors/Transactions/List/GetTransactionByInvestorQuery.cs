using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.DTOs.Investors;

namespace FundAdministration.UseCases.Investors.List;

public record GetTransactionByInvestorQuery(Guid guid) : IQuery<Result<IEnumerable<InvestorListDTO>>>;
