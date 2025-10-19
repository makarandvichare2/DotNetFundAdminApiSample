using Asp.Versioning;
using FundAdministration.API.Extensions;
using FundAdministration.Common.Transactions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FundAdministration.UseCases.Reports.List;

namespace FundAdministration.API.Controllers.Reports;

/// <summary>
/// Provides reporting endpoints related to fund administration.
/// </summary>
[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
[ControllerName("Report")]
[Authorize]
public class ReportV1Controller : ControllerBase
{
    private readonly IMediator mediator;
    public ReportV1Controller(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Retrieves the net investment amount per fund.
    /// </summary>
    /// <returns>
    /// A list of funds with their respective net investment amounts.
    /// </returns>
    [HttpGet("NetInvestmentPerFund")]
    [ProducesResponseType(typeof(TransactionListDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNetInvestmentPerFundAsync()
    {
        var result = await mediator.Send(new GetNetInvestmentPerFundQuery());

        return result.ToActionResult(this);
    }

    /// <summary>
    /// Retrieves the total number of investors for each fund.
    /// </summary>
    /// <returns>
    /// A list of funds with their corresponding total investor counts.
    /// </returns>
    [HttpGet("TotalInvestorsByFund")]
    [ProducesResponseType(typeof(TransactionListDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTotalInvestorsByFundAsync()
    {
        var result = await mediator.Send(new GetTotalInvestorsByFundQuery());

        return result.ToActionResult(this);
    }

}