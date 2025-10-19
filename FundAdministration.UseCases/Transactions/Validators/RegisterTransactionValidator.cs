using Ardalis.SharedKernel;
using FluentValidation;
using FundAdministration.Core.Investors;
using FundAdministration.UseCases.Specifications;
using FundAdministration.UseCases.Transactions.Register;

namespace FundAdministration.UseCases.Investors.Validators;
public class RegisterTransactionValidator : AbstractValidator<RegisterTransactionCommand>
{
    private readonly IReadRepository<Investor> _investorRepository;
    public RegisterTransactionValidator(IReadRepository<Investor> investorRepository)
    {
        _investorRepository = investorRepository;

        RuleFor(x => x.investorId)
        .NotEmpty()
        .Must(id => id != Guid.Empty)
        .WithMessage("Investor id is required and must be a valid GUID.")
        .MustAsync(IsInvestorIdValid)
        .WithMessage("Invalid investor id");

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

    private async Task<bool> IsInvestorIdValid(Guid investorId, CancellationToken token)
    {
        var count = await _investorRepository.CountAsync(new InvestorIdExistSpec(investorId), token);
        return count <= 0;
    }
}
