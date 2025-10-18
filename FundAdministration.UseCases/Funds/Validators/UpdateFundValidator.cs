using FluentValidation;
using FundAdministration.Infrastructure.Data.Config;
using FundAdministration.UseCases.Funds.Update;

namespace FundAdministration.UseCases.Funds.Validators;
public class UpdateFundValidator : AbstractValidator<UpdateFundCommand>
{
    public UpdateFundValidator()
    {
        RuleFor(x => x.fundName.Value)
         .NotEmpty()
         .WithMessage("Fund Name is required.")
         .MaximumLength(DataSchemaConstants.LENGTH_100);

        RuleFor(x => x.currencyCode.Value)
         .NotEmpty()
         .WithMessage("Currency Code is required.")
         .MaximumLength(DataSchemaConstants.LENGTH_3)
         .MinimumLength(DataSchemaConstants.LENGTH_3);

        RuleFor(x => x.launchDate)
         .NotNull()
         .WithMessage("Launch Date is required.");

    }
}
