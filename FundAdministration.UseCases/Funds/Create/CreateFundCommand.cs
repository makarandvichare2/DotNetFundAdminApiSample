using Ardalis.Result;
using Ardalis.SharedKernel;

namespace FundAdministration.UseCases.Funds.Create;
public record CreateFundCommand
        (string fundName,
        string currencyCode,
        DateTime launchDate
         ) : ICommand<Result<Guid>>;

