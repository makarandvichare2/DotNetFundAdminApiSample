using Ardalis.Result;
using Ardalis.SharedKernel;

namespace FundAdministration.UseCases.Funds.Delete;
public record DeleteFundCommand( Guid id ) : ICommand<Result<bool>>;

