namespace FundAdministration.UseCases.Investors.List;

public record InvestorListDTO(
    string fullName,
    string emailId,
    int fundId);
