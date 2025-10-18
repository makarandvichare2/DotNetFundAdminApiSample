using FundAdministration.UseCases.Funds.Create;
using FluentValidation;
using FundAdministration.UseCases.Funds.Delete;
using FundAdministration.UseCases.Investors.Delete;

namespace FundAdministration.UseCases.Funds.Validators;
public class DeleteInvestorValidator : AbstractValidator<DeleteInvestorCommand>
{
    public DeleteInvestorValidator()
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
