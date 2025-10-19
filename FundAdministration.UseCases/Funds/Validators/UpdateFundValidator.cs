using Ardalis.SharedKernel;
using FluentValidation;
using FundAdministration.Core.Funds;
using FundAdministration.Infrastructure.Data.Config;
using FundAdministration.UseCases.Funds.Update;
using FundAdministration.UseCases.Specifications;

namespace FundAdministration.UseCases.Funds.Validators;
public class UpdateFundValidator : AbstractValidator<UpdateFundCommand>
{
    private readonly IReadRepository<Fund> _repository;
    public UpdateFundValidator(IReadRepository<Fund> repository)
    {
        _repository = repository;

        RuleFor(x => x.fundName.Value)
        .NotEmpty()
        .WithMessage("Fund Name is required.")
        .MaximumLength(DataSchemaConstants.LENGTH_100)
        .MustAsync(BeUniqueName)
        .WithMessage("Fund name already exists.");


        RuleFor(x => x.currencyCode.Value)
         .NotEmpty()
         .WithMessage("Currency Code is required.")
         .MaximumLength(DataSchemaConstants.LENGTH_3)
         .MinimumLength(DataSchemaConstants.LENGTH_3);

        RuleFor(x => x.launchDate.Value)
         .NotNull()
         .WithMessage("Launch Date is required.");

    }

    private async Task<bool> BeUniqueName(UpdateFundCommand command, string fundName, CancellationToken token)
    {
        var existing = await _repository.FirstOrDefaultAsync(new FundByNameSpec(fundName), token);
        return (existing is null) ? true : existing.Id == command.id;
    }
}
