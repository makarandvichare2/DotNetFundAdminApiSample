namespace FundAdministration.DTOs;

public record LookUpDTO<T>(
    T Id,
    string Name);
