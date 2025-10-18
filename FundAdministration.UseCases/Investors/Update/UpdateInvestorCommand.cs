using Ardalis.Result;
using Ardalis.SharedKernel;
using FundAdministration.DTOs;

namespace FundAdministration.UseCases.Investors.Update;
public record UpdateInvestorCommand
        (
            Guid guid,
            Optional<string> fullName,
            Optional<string> emailId,
            Optional<int> fundId
         ) : ICommand<Result<bool>>;

