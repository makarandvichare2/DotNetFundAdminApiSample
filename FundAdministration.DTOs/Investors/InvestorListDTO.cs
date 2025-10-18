namespace FundAdministration.DTOs.Investors;

public record InvestorListDTO(
    Guid guid,
    string fullName,
    string emailId,
    string fundName);
