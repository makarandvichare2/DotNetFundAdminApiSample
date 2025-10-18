using Ardalis.Result;
using Ardalis.SharedKernel;

namespace FundAdministration.UseCases.Funds.Delete;
public record DeleteFundCommand( Guid guid ) : ICommand<Result<bool>>;

