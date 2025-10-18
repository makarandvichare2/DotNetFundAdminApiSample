namespace FundAdministration.UseCases.Funds.List;

public record FundListDTO(
    string fundName,
    string currency,
    DateTime launchDate);
