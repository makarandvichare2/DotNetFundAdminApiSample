using Ardalis.Specification;
using FundAdministration.Core.Funds;

namespace FundAdministration.UseCases.Specifications;

public class FundByNameSpec : Specification<Fund>
{
    public FundByNameSpec(string fundName)
    {
        Query.Where(f => f.FundName == fundName);
    }
}
