using Ardalis.Specification;
using FundAdministration.Core.Investors;

namespace FundAdministration.UseCases.Specifications;

public class InvestorIdExistSpec : Specification<Investor>
{
    public InvestorIdExistSpec(Guid investorId)
    {
        Query.Where(f => f.Id == investorId);
    }
}
