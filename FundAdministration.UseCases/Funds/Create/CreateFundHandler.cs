using Ardalis.Result;
using Ardalis.SharedKernel;
using FluentValidation;
using FundAdministration.Core.Funds;

namespace FundAdministration.UseCases.Funds.Create;

public class CreateFundHandler(IRepository<Fund> _repository,
    IValidator<CreateFundCommand> _validator)
  : ICommandHandler<CreateFundCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateFundCommand request,
      CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        var newItem = new Fund(
        request.fundName,
        new Currency(request.currencyCode),
        request.launchDate
        );
        var createdItem = await _repository.AddAsync(newItem, cancellationToken);

        return createdItem.GuId;
    }
}
