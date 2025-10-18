namespace FundAdministration.UseCases.Funds.List;

public record FundListDTO(
    string fundName,
    string currencyCode,
    DateTime launchDate);
