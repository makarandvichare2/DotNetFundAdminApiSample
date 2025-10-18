using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.SharedKernel;
using FluentValidation;
using FundAdministration.Core.Funds;

namespace FundAdministration.UseCases.Funds.Update;

public class UpdateFundHandler(IRepository<Fund> _repository,
    IValidator<UpdateFundCommand> _validator)
  : ICommandHandler<UpdateFundCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateFundCommand request,
      CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        var existingFund = await _repository.GetByIdAsync(request.guid, cancellationToken);

        Guard.Against.Null(existingFund, nameof(existingFund));

        if (request.fundName.HasValue)
        {
            existingFund.UpdateFundName(request.fundName.Value);
        }

        if (request.currencyCode.HasValue)
        {
            existingFund.UpdateCurrency(request.currencyCode.Value);
        }

        if (request.launchDate.HasValue)
        {
            existingFund.UpdateLaunchDate(request.launchDate.Value);
        }    

        await _repository.UpdateAsync(existingFund, cancellationToken);

        return true;
    }
}
