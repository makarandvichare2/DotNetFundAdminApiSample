using Ardalis.GuardClauses;
using Ardalis.Result;
using Ardalis.SharedKernel;
using FluentValidation;
using FundAdministration.Core.Funds;
using FundAdministration.Core.Investors;

namespace FundAdministration.UseCases.Investors.Delete;

public class DeleteInvestorHandler(IRepository<Investor> _repository,
    IValidator<DeleteInvestorCommand> _validator)
  : ICommandHandler<DeleteInvestorCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteInvestorCommand request,
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
