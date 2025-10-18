using FluentValidation;
using FundAdministration.UseCases.Investors.Delete;

namespace FundAdministration.UseCases.Funds.Validators;
public class DeleteInvestorValidator : AbstractValidator<DeleteInvestorCommand>
{
    public DeleteInvestorValidator()
    {
        RuleFor(x => x.id)
        .NotEmpty()
        .Must(id => id != Guid.Empty)
        .WithMessage("Investor id is required and must be a valid GUID."); ;

    }
}
