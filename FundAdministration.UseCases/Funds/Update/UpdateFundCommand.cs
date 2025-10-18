using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.Common;

namespace FundAdministration.UseCases.Funds.Update;
public record UpdateFundCommand
        (
        Guid id,
        Optional<string> fundName,
        Optional<string> currencyCode,
        Optional<DateTime> launchDate
         ) : ICommand<Result<bool>>;

