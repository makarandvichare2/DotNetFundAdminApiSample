namespace FundAdministration.UseCases.Funds.Get;

public record CreateFundDTO(
    string fundName,
    string currency,
    DateTime launchDate
);
