namespace FundAdministration.DTOs.Funds;

public record CreateFundDataDTO(
    CreateFundDTO fund,
    List<string> currencies
    );
