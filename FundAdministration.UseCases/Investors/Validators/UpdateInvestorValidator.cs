using FluentValidation;
using FundAdministration.Infrastructure.Data.Config;
using FundAdministration.UseCases.Investors.Update;

namespace FundAdministration.UseCases.Investors.Validators;
public class UpdateInvestorValidator : AbstractValidator<UpdateInvestorCommand>
{
    public UpdateInvestorValidator()
    {
        RuleFor(x => x.fullName.Value)
         .NotEmpty()
         .WithMessage("Fund Name is required.")
         .MaximumLength(DataSchemaConstants.LENGTH_100);

        RuleFor(x => x.emailId)
         .NotEmpty()
         .WithMessage("Currency Code is required.");

        RuleFor(x => x.fundId.Value)
        .NotEmpty()
        .Must(id => id != Guid.Empty)
        .WithMessage("Fund id is required and must be a valid GUID.");

    }
}
