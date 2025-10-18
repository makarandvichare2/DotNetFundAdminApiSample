using Ardalis.Result;
using Ardalis.SharedKernel;
using FluentValidation;
using FundAdministration.Core.Transactions;
using FundAdministration.Infrastructure.Data;

namespace FundAdministration.UseCases.Transactions.Register;

public class RegisterTransactionHandler(IEfRepository<Transaction> _repository,
    IValidator<RegisterTransactionCommand> _validator)
  : ICommandHandler<RegisterTransactionCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterTransactionCommand request,
      CancellationToken cancellationToken)
    {
        try
        {
            _validator.ValidateAndThrow(request);

            var newItem = new Transaction(
                request.investorId,
                request.transactionType,
               request.amount,
               request.transactionDate
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
