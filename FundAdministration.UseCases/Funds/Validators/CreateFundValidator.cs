using FundAdministration.UseCases.Funds.Create;
using FluentValidation;
using FundAdministration.Infrastructure.Data.Config;

namespace FundAdministration.UseCases.Funds.Validators;
public class CreateFundValidator : AbstractValidator<CreateFundCommand>
{
    public CreateFundValidator()
    {
        RuleFor(x => x.fundName)
         .NotEmpty()
         .WithMessage("Fund Name is required.")
         .MaximumLength(DataSchemaConstants.LENGTH_100);

        RuleFor(x => x.currencyCode)
         .NotEmpty()
         .WithMessage("Currency Code is required.")
         .MaximumLength(DataSchemaConstants.LENGTH_3)
         .MinimumLength(DataSchemaConstants.LENGTH_3);

        RuleFor(x => x.launchDate)
         .NotNull()
         .WithMessage("Launch Date is required.");
    }
}
