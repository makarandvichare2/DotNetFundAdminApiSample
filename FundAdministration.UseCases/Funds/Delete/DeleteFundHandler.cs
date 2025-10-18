using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.SharedKernel;
using FluentValidation;
using FundAdministration.Core.Funds;

namespace FundAdministration.UseCases.Funds.Delete;

public class DeleteFundHandler(IRepository<Fund> _repository,
    IValidator<DeleteFundCommand> _validator)
  : ICommandHandler<DeleteFundCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteFundCommand request,
      CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        var existingFund = await _repository.GetByIdAsync(request.guid, cancellationToken);

        Guard.Against.Null(existingFund, nameof(existingFund));

        existingFund.UpdateIsDeleted(true);

        await _repository.UpdateAsync(existingFund, cancellationToken);

        return true;
    }
}
