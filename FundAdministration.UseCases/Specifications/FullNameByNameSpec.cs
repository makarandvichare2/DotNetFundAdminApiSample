using Ardalis.Specification;
using FundAdministration.Core.Investors;

namespace FundAdministration.UseCases.Specifications;

public class FullNameByNameSpec : Specification<Investor>
{
    public FullNameByNameSpec(string fullName)
    {
        Query.Where(f => f.FullName == fullName);
    }
}
