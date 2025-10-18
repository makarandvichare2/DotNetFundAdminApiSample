namespace FundAdministration.UseCases.Common;

public record LookUpDTO<T>(
    T Id,
    string Name);
