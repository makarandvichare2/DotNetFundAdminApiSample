using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.Common;

namespace FundAdministration.UseCases.Investors.Update;
public record UpdateInvestorCommand
        (
        Optional<string> guid,
        Optional<string> fullName,
        Optional<string> emailId,
        Optional<int> fundId
         ) : ICommand<Result<bool>>;

