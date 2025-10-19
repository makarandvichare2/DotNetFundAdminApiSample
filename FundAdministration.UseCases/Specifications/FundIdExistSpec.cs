using Ardalis.Specification;
using FundAdministration.Core.Funds;

namespace FundAdministration.UseCases.Specifications;

public class FundIdExistSpec : Specification<Fund>
{
    public FundIdExistSpec(Guid fundId)
    {
        Query.Where(f => f.Id == fundId);
    }
}
