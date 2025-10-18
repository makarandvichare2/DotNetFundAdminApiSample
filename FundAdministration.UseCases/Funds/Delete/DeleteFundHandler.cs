using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.SharedKernel;
using FluentValidation;
using FundAdministration.Core.Funds;
using FundAdministration.Infrastructure.Data;

namespace FundAdministration.UseCases.Funds.Delete;

public class DeleteFundHandler(IEfRepository<Fund> _repository,
    IValidator<DeleteFundCommand> _validator)
  : ICommandHandler<DeleteFundCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteFundCommand request,
      CancellationToken cancellationToken)
    {
        try
        {
            _validator.ValidateAndThrow(request);

            var existingFund = await _repository.GetByIdAsync(request.id, cancellationToken);

            Guard.Against.Null(existingFund, nameof(existingFund));

            existingFund.UpdateIsDeleted(true);

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
