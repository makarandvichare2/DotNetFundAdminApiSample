using Ardalis.SharedKernel;
using FluentValidation;
using FundAdministration.Core.Funds;
using FundAdministration.Core.Investors;
using FundAdministration.Infrastructure.Data.Config;
using FundAdministration.UseCases.Investors.Update;
using FundAdministration.UseCases.Specifications;

namespace FundAdministration.UseCases.Investors.Validators;
public class UpdateInvestorValidator : AbstractValidator<UpdateInvestorCommand>
{
    private readonly IReadRepository<Investor> _investorRepository;
    private readonly IReadRepository<Fund> _fundRepository;
    public UpdateInvestorValidator(IReadRepository<Investor> investorRepository, IReadRepository<Fund> fundRepository)
    {
        _investorRepository = investorRepository;
        _fundRepository = fundRepository;

        RuleFor(x => x.fullName.Value)
        .NotEmpty()
        .WithMessage("Fund Name is required.")
        .MaximumLength(DataSchemaConstants.LENGTH_100)
        .MustAsync(BeUniqueName)
        .WithMessage("Full name already exists.");

        RuleFor(x => x.emailId)
         .NotEmpty()
         .WithMessage("Currency Code is required.");

        RuleFor(x => x.fundId.Value)
        .NotEmpty()
        .Must(id => id != Guid.Empty)
        .WithMessage("Fund id is required and must be a valid GUID.")
        .MustAsync(IsFundIdValid)
        .WithMessage("Invalid fund id"); 

    }

    private async Task<bool> BeUniqueName(UpdateInvestorCommand command,string fullName, CancellationToken token)
    {
        var existing = await _investorRepository.FirstOrDefaultAsync(new FullNameByNameSpec(fullName), token);
        return (existing is null) ? true : existing.Id == command.id;
    }
    private async Task<bool> IsFundIdValid(Guid fundId, CancellationToken token)
    {
        var count = await _fundRepository.CountAsync(new FundIdExistSpec(fundId), token);
        return count <= 0;
    }
}
