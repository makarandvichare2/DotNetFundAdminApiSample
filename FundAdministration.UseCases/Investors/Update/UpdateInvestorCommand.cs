using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.Common;

namespace FundAdministration.UseCases.Investors.Update;
public record UpdateInvestorCommand
        (
            Guid id,
            Optional<string> fullName,
            Optional<string> emailId,
            Optional<Guid> fundId
         ) : ICommand<Result<bool>>;

