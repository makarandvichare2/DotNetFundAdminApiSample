namespace FundAdministration.Common.Funds;

public record CreateFundDataDTO(
    CreateFundDTO fund,
    List<string> currencies
    );
