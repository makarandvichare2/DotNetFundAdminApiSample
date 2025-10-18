using Ardalis.Result;
using Ardalis.SharedKernel;
using FluentValidation;
using FundAdministration.Core.Funds;
using FundAdministration.Core.Investors;
using FundAdministration.UseCases.Investors.Create;

namespace FundAdministration.UseCases.Investors.Create;

public class CreateInvestorHandler(IRepository<Investor> _repository,
    IValidator<CreateInvestorCommand> _validator)
  : ICommandHandler<CreateInvestorCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateInvestorCommand request,
      CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        var newItem = new Investor(
        request.fullName,
        new Email(request.emailId),
        request.fundId
        );
        var createdItem = await _repository.AddAsync(newItem, cancellationToken);

        return createdItem.GuId;
    }
}
