using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.SharedKernel;
using FluentValidation;
using FundAdministration.Core.Funds;
using FundAdministration.Core.Investors;
using FundAdministration.Infrastructure.Data;

namespace FundAdministration.UseCases.Investors.Delete;

public class DeleteInvestorHandler(IEfRepository<Investor> _repository,
    IValidator<DeleteInvestorCommand> _validator)
  : ICommandHandler<DeleteInvestorCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteInvestorCommand request,
      CancellationToken cancellationToken)
    {
        try
        {
            _validator.ValidateAndThrow(request);

            var existingFund = await _repository.GetByGuidAsync(request.guid, cancellationToken);

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
