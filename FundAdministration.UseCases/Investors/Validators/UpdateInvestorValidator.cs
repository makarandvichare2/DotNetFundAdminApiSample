using FluentValidation;
using FundAdministration.UseCases.Investors.Update;

namespace FundAdministration.UseCases.Investors.Validators;
public class UpdateInvestorValidator : AbstractValidator<UpdateInvestorCommand>
{
    public UpdateInvestorValidator()
    {
        //RuleFor(x => x.fundName)
        // .NotEmpty()
        // .WithMessage("Fund Name is required.");
        //RuleFor(x => x.currencyCode)
        // .NotEmpty()
        // .WithMessage("Currency Code is required.")
        // .MaximumLength(3)
        // .MinimumLength(3);
        //RuleFor(x => x.launchDate)
        // .NotNull()
        // .WithMessage("Launch Date is required.");

    }
}
