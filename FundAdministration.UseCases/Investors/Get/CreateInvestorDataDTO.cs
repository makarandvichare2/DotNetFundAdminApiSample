using FundAdministration.UseCases.Common;

namespace FundAdministration.UseCases.Investors.Get;

public record CreateInvestorDataDTO(
    CreateInvestorDTO investor,
    List<LookUpDTO<int>> funds
    );
