using FluentValidation;
using FundAdministration.UseCases.Transactions.Register;

namespace FundAdministration.UseCases.Investors.Validators;
public class RegisterTransactionValidator : AbstractValidator<RegisterTransactionCommand>
{
    public RegisterTransactionValidator()
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
