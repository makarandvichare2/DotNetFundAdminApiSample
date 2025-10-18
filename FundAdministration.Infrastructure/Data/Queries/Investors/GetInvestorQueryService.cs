using FundAdministration.DTOs;
using FundAdministration.DTOs.Investors;
using Microsoft.EntityFrameworkCore;

namespace FundAdministration.Infrastructure.Data.Queries.Investors;

public class GetInvestorQueryService(AppDbContext _db) : IGetInvestorQueryService
{
    public async Task<CreateInvestorDataDTO> InvestorDataAsync(Guid guid)
    {

        var funds = await _db.Database.SqlQuery<LookUpDTO<int>>( $"SELECT Id, FundName as Name FROM Funds Order by FundName").ToListAsync();

        var investor = await _db.Investors.AsNoTracking().FirstOrDefaultAsync(o => o.GuId == guid);

        var fundId = (investor != null) ? investor.FundId : 0;
        var fullName = investor?.FullName ?? string.Empty;
        var emailId = investor?.Email?.EmailId ?? string.Empty;
        var fundName = funds.FirstOrDefault(o=>o.Id == fundId)?.Name ?? string.Empty;

        var investorDto = new CreateInvestorDTO(fullName, emailId, fundId, fundName);

        var createInvestorDto = new CreateInvestorDataDTO(investorDto, funds);

        return createInvestorDto;
    }
}
