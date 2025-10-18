namespace FundAdministration.Common.Investors;

public record InvestorListDTO(
    Guid id,
    string fullName,
    string emailId,
    string fundName);
