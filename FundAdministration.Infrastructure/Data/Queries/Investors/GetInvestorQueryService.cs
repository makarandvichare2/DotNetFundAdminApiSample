using FundAdministration.Common;
using FundAdministration.Common.Investors;
using Microsoft.EntityFrameworkCore;

namespace FundAdministration.Infrastructure.Data.Queries.Investors;

public class GetInvestorQueryService(AppDbContext _db) : IGetInvestorQueryService
{
    public async Task<CreateInvestorDataDTO> InvestorDataAsync(Guid guid)
    {

        var funds = await _db.Database.SqlQuery<LookUpDTO<Guid>>( 
            @$"SELECT 
                    Id, FundName as Name 
               FROM Funds 
               WHERE IsDeleted = 0 
               ORDER BY FundName").ToListAsync();

        var investor = await _db.Investors.AsNoTracking().FirstOrDefaultAsync(o => o.Id == guid);

        var fundId = (investor != null) ? investor.FundId : Guid.Empty;
        var fullName = investor?.FullName ?? string.Empty;
        var emailId = investor?.Email?.EmailId ?? string.Empty;
        var fundName = funds.FirstOrDefault(o=>o.Id == fundId)?.Name ?? string.Empty;

        var investorDto = new CreateInvestorDTO(fullName, emailId, fundId, fundName);

        var createInvestorDto = new CreateInvestorDataDTO(investorDto, funds);

        return createInvestorDto;
    }
}
