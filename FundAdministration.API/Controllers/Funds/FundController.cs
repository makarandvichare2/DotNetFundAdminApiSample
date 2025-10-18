using FundAdministration.API.Extensions;
using FundAdministration.DTOs.Funds;
using FundAdministration.UseCases.Funds.Create;
using FundAdministration.UseCases.Funds.Delete;
using FundAdministration.UseCases.Funds.Get;
using FundAdministration.UseCases.Funds.List;
using FundAdministration.UseCases.Funds.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace FundAdministration.API.Controllers.Funds;

[ApiController]
[Route("[controller]")]
public class FundController : ControllerBase
{
    private readonly IMediator mediator;
    public FundController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<FundListDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAsync()
    {
        var result = await mediator.Send(new ListFundQuery());

        return result.ToActionResult();
    }

    [HttpGet("{guid:guid}")]
    [ProducesResponseType(typeof(CreateFundDataDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFundAsync(Guid guid)
    {
        var result = await mediator.Send(new GetFundQuery(guid));

        return result.ToActionResult();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateFund(
            [FromBody] CreateFundRequest request)
    {
        var command = new CreateFundCommand(
            request.fundName,
            request.currencyCode,
            request.launchDate
            );
        var result = await mediator.Send(command);

        return result.ToActionResult();
    }

    [HttpDelete("{guid:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFund(Guid guid)
    {
        var command = new DeleteFundCommand(guid);
        var result = await mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPut("{guid:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateFund(Guid guid, [FromBody] UpdateFundRequest request)
    {
        var command = new UpdateFundCommand(
            guid,
            request.fundName,
            request.currencyCode,
            request.launchDate
            );
        var result = await mediator.Send(command);

        return result.ToActionResult();
    }
}