using Ardalis.Result;
using Ardalis.SharedKernel;
using FluentValidation;
using FundAdministration.Core.Funds;
using FundAdministration.Infrastructure.Data;

namespace FundAdministration.UseCases.Funds.Create;

public class CreateFundHandler(ISoftDeleteRepository<Fund> _repository,
    IValidator<CreateFundCommand> _validator)
  : ICommandHandler<CreateFundCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateFundCommand request,
      CancellationToken cancellationToken)
    {
        try
        {
            _validator.ValidateAndThrow(request);

            var newItem = new Fund(
            request.fundName,
            new Currency(request.currencyCode),
            request.launchDate
            );
            var createdItem = await _repository.AddAsync(newItem, cancellationToken);

            return Result.Success(createdItem.Id);
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
