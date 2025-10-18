namespace FundAdministration.Common.Funds;

public record CreateFundDTO(
    string fundName,
    string currency,
    DateTime launchDate
);
