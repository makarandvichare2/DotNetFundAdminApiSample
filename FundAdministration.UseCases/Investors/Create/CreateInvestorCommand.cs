using Ardalis.Result;
using Ardalis.SharedKernel;

namespace FundAdministration.UseCases.Investors.Create;
public record CreateInvestorCommand
        (string fullName,
        string emailId,
        int fundId
         ) : ICommand<Result<Guid>>;

