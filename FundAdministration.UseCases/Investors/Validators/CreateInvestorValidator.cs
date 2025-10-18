using FluentValidation;
using FundAdministration.Infrastructure.Data.Config;
using FundAdministration.UseCases.Investors.Create;

namespace FundAdministration.UseCases.Investors.Validators;
public class CreateInvestorValidator : AbstractValidator<CreateInvestorCommand>
{
    public CreateInvestorValidator()
    {
        RuleFor(x => x.fullName)
         .NotEmpty()
         .WithMessage("Fund Name is required.")
         .MaximumLength(DataSchemaConstants.LENGTH_100);

        RuleFor(x => x.emailId)
         .NotEmpty()
         .WithMessage("Currency Code is required.");

        RuleFor(x => x.fundId)
            .NotEmpty()
            .Must(id => id != Guid.Empty)
            .WithMessage("Fund id is required and must be a valid GUID.");

    }
}
