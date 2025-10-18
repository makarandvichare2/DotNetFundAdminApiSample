namespace FundAdministration.UseCases.Investors.Get;

public record CreateInvestorDTO(
    string fullName,
    string emailId,
    int fundId
);
