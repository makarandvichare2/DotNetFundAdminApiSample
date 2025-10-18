using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.SharedKernel;
using FluentValidation;
using FundAdministration.Core.Funds;
using FundAdministration.Infrastructure.Data;

namespace FundAdministration.UseCases.Funds.Update;

public class UpdateFundHandler(IEfRepository<Fund> _repository,
    IValidator<UpdateFundCommand> _validator)
  : ICommandHandler<UpdateFundCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateFundCommand request,
      CancellationToken cancellationToken)
    {
        try
        {
            _validator.ValidateAndThrow(request);

            var existingFund = await _repository.GetByGuidAsync(request.id, cancellationToken);

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

            return Result.Success(true);
        }
        catch (ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new ValidationError(e.PropertyName, e.ErrorMessage));
            return Result.Invalid(errors);
        }
        catch (Exception ex)
        {
            // _logger.LogError(ex, "Error creating fund");
            return Result.Error(ex.Message);
        }
    }
}
