using FluentValidation;
using FundAdministration.UseCases.Transactions.Register;

namespace FundAdministration.UseCases.Investors.Validators;
public class RegisterTransactionValidator : AbstractValidator<RegisterTransactionCommand>
{
    public RegisterTransactionValidator()
    {
        RuleFor(x => x.investorId)
        .NotEmpty()
        .Must(id => id != Guid.Empty)
        .WithMessage("Investor id is required and must be a valid GUID.");

        RuleFor(x => x.transactionType)
         .IsInEnum()
         .WithMessage("Transaction Type is required.");

        RuleFor(x => x.amount)
         .GreaterThanOrEqualTo(0)
         .WithMessage("Amount must be greater than zero.")
         .LessThan(int.MaxValue)
         .WithMessage("Amount exceeds the allowed limit.");

        RuleFor(x => x.transactionDate)
         .NotNull()
         .WithMessage("Transaction Date is required.");

    }
}
