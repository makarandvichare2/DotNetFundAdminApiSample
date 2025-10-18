using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.SharedKernel;
using FluentValidation;
using FundAdministration.Core.Investors;
using FundAdministration.Infrastructure.Data;

namespace FundAdministration.UseCases.Investors.Update;

public class UpdateInvestorHandler(IEfRepository<Investor> _repository,
    IValidator<UpdateInvestorCommand> _validator)
  : ICommandHandler<UpdateInvestorCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateInvestorCommand request,
      CancellationToken cancellationToken)
    {
        try
        {
            _validator.ValidateAndThrow(request);

            var existingFund = await _repository.GetByGuidAsync(request.id, cancellationToken);

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
