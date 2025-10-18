namespace FundAdministration.Common;

public record LookUpDTO<T>(
    T Id,
    string Name);
