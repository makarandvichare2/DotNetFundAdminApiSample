using Ardalis.Result;
using Ardalis.SharedKernel;
using FluentValidation;
using FundAdministration.Core.Investors;
using FundAdministration.Infrastructure.Data;

namespace FundAdministration.UseCases.Investors.Create;

public class CreateInvestorHandler(IEfRepository<Investor> _repository,
    IValidator<CreateInvestorCommand> _validator)
  : ICommandHandler<CreateInvestorCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateInvestorCommand request,
      CancellationToken cancellationToken)
    {
        try
        {
            _validator.ValidateAndThrow(request);

            var newItem = new Investor(
                request.fullName,
                new Email(request.emailId),
                request.fundId
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
