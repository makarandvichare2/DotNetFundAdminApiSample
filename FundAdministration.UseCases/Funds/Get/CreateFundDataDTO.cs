namespace FundAdministration.UseCases.Funds.Get;

public record CreateFundDataDTO(
    CreateFundDTO fund,
    List<string> currencies
    );
