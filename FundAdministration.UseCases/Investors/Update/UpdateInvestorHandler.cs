using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.SharedKernel;
using FluentValidation;
using FundAdministration.Core.Funds;
using FundAdministration.Core.Investors;

namespace FundAdministration.UseCases.Investors.Update;

public class UpdateInvestorHandler(IRepository<Investor> _repository,
    IValidator<UpdateInvestorCommand> _validator)
  : ICommandHandler<UpdateInvestorCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateInvestorCommand request,
      CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        var existingFund = await _repository.GetByIdAsync(request.guid, cancellationToken);

        Guard.Against.Null(existingFund, nameof(existingFund));

        if (request.fullName.HasValue)
        {
            existingFund.UpdateFullName(request.fullName.Value);
        }

        if (request.emailId.HasValue)
        {
            existingFund.UpdateEmail(request.emailId.Value);
        }

        if (request.fundId.HasValue)
        {
            existingFund.UpdateFundId(request.fundId.Value);
        }    

        await _repository.UpdateAsync(existingFund, cancellationToken);

        return true;
    }
}
