namespace FundAdministration.Common.Investors;

public record CreateInvestorDTO(
    string fullName,
    string emailId,
    int fundId,
    string fundName
);
