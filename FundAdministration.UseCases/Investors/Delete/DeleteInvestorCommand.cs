using Ardalis.Result;
using Ardalis.SharedKernel;

namespace FundAdministration.UseCases.Investors.Delete;
public record DeleteInvestorCommand( Guid guid ) : ICommand<Result<bool>>;

