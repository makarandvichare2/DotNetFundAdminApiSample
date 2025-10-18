namespace FundAdministration.DTOs.Investors;

public record CreateInvestorDataDTO(
    CreateInvestorDTO investor,
    List<LookUpDTO<int>> funds
    );
