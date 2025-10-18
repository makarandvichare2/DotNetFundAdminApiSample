namespace FundAdministration.Common.Investors;

public record CreateInvestorDTO(
    string fullName,
    string emailId,
    Guid fundId,
    string fundName
);
