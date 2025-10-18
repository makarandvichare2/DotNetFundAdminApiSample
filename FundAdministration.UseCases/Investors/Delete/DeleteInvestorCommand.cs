using Ardalis.Result;
using Ardalis.SharedKernel;

namespace FundAdministration.UseCases.Investors.Delete;
public record DeleteInvestorCommand( Guid id ) : ICommand<Result<bool>>;

