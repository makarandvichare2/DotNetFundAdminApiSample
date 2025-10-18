namespace FundAdministration.Common.Funds;

public record FundListDTO(
    Guid guid,
    string fundName,
    string currencyCode,
    DateTime launchDate);
