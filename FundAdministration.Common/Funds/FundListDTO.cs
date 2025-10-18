namespace FundAdministration.Common.Funds;

public record FundListDTO(
    Guid id,
    string fundName,
    string currencyCode,
    DateTime launchDate);
