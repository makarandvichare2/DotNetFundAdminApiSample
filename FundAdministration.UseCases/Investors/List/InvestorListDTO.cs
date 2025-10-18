namespace FundAdministration.UseCases.Investors.List;

public record InvestorListDTO(
    Guid guid,
    string fullName,
    string emailId,
    int fundId);
