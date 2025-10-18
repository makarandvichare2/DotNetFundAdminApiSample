namespace FundAdministration.Common.Investors;

public record CreateInvestorDataDTO(
    CreateInvestorDTO investor,
    List<LookUpDTO<int>> funds
    );
