namespace FundAdministration.Common.Investors;

public record CreateInvestorDataDTO(
    CreateInvestorDTO investor,
    List<LookUpDTO<Guid>> funds
    );
