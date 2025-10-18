using FundAdministration.Common;
using FundAdministration.Common.Investors;
using Microsoft.EntityFrameworkCore;

namespace FundAdministration.Infrastructure.Data.Queries.Funds;

public class GetInvestorQueryService(AppDbContext _db) : IGetInvestorQueryService
{
    public async Task<CreateInvestorDataDTO> InvestorDataAsync(Guid guid)
    {

        var funds = await _db.Database.SqlQuery<LookUpDTO<int>>( $"SELECT Id, Name FROM Funds Order by FundName").ToListAsync();

        var investor = await _db.Investors.AsNoTracking().FirstOrDefaultAsync(o => o.GuId == guid);

        var fundId = (investor != null) ? investor.FundId : 0;
        var fullName = investor?.FullName ?? string.Empty;
        var emailId = investor?.Email?.EmailId ?? string.Empty;

        var investorDto = new CreateInvestorDTO(fullName, emailId, fundId);

        var createInvestorDto = new CreateInvestorDataDTO(investorDto, funds);

        return createInvestorDto;
    }
}
