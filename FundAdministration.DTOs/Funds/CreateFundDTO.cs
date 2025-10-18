namespace FundAdministration.DTOs.Funds;

public record CreateFundDTO(
    string fundName,
    string currency,
    DateTime launchDate
);
