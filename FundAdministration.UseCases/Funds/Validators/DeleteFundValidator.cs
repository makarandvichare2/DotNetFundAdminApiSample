using FluentValidation;
using FundAdministration.UseCases.Funds.Delete;

namespace FundAdministration.UseCases.Funds.Validators;
public class DeleteFundValidator : AbstractValidator<DeleteFundCommand>
{
    public DeleteFundValidator()
    {
        RuleFor(x => x.id)
        .NotEmpty()
        .Must(id => id != Guid.Empty)
        .WithMessage("Fund id is required and must be a valid GUID.");
    }
}
