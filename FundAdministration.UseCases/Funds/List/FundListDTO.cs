namespace FundAdministration.UseCases.Funds.List;

public record FundListDTO(
    Guid guid,
    string fundName,
    string currencyCode,
    DateTime launchDate);
