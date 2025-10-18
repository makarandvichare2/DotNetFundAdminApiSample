using Ardalis.Result;
using Ardalis.SharedKernel;

namespace FundAdministration.UseCases.Investors.Create;
public record CreateInvestorCommand
        (string fullName,
        string emailId,
        Guid fundId
         ) : ICommand<Result<Guid>>;

