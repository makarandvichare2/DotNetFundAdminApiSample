namespace FundAdministration.DTOs.Investors;

public record CreateInvestorDTO(
    string fullName,
    string emailId,
    int fundId,
    string fundName
);
