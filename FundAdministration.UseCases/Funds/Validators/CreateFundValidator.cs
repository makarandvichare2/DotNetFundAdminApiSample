using Ardalis.SharedKernel;
using FluentValidation;
using FundAdministration.Core.Funds;
using FundAdministration.Infrastructure.Data.Config;
using FundAdministration.UseCases.Funds.Create;
using FundAdministration.UseCases.Specifications;

namespace FundAdministration.UseCases.Funds.Validators;
public class CreateFundValidator : AbstractValidator<CreateFundCommand>
{
    private readonly IReadRepository<Fund> _repository;
    public CreateFundValidator(IReadRepository<Fund> repository)
    {
        _repository = repository;

        RuleFor(x => x.fundName)
         .NotEmpty()
         .WithMessage("Fund Name is required.")
         .MaximumLength(DataSchemaConstants.LENGTH_100)
         .MustAsync(BeUniqueName)
        .WithMessage("Fund name already exists.");

        RuleFor(x => x.currencyCode)
         .NotEmpty()
         .WithMessage("Currency Code is required.")
         .MaximumLength(DataSchemaConstants.LENGTH_3)
         .MinimumLength(DataSchemaConstants.LENGTH_3);

        RuleFor(x => x.launchDate)
         .NotNull()
         .WithMessage("Launch Date is required.");
    }

    private async Task<bool> BeUniqueName(string fundName, CancellationToken token)
    {
       var count = await _repository.CountAsync(new FundByNameSpec(fundName), token);
       return count <= 0;
    }
}
