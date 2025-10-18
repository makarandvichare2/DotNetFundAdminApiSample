using Ardalis.Result;
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
    public async Task<ActionResult<List<FundListDTO>>> GetListAsync()
    {
        Result<IEnumerable<FundListDTO>> result = await mediator.Send(new ListFundQuery());

        if (result.IsSuccess)
        {
            return Ok(result.Value.ToList());
        }

        return NotFound();
    }

    [HttpGet]
    [Route("GetFund/{guid}")]
    [ProducesResponseType(typeof(CreateFundDataDTO), StatusCodes.Status200OK)]
    public async Task<ActionResult<CreateFundDataDTO>> GetFundAsync(Guid guid)
    {
        Result<CreateFundDataDTO> result = await mediator.Send(new GetFundQuery(guid));

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateFund(
            [FromBody] CreateFundRequest request)
    {
        var command = new CreateFundCommand(
            request.fundName,
            request.currencyCode,
            request.launchDate
            );
        var result = await mediator.Send(command);

        return Created(string.Empty, result.Value);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteFund(Guid fundGuid)
    {
        var command = new DeleteFundCommand(fundGuid);
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateFund(
        [FromBody] UpdateFundRequest request)
    {
        var command = new UpdateFundCommand(
            request.guid,
            request.fundName,
            request.currencyCode,
            request.launchDate
            );
        var result = await mediator.Send(command);

        return Created(string.Empty, result.Value);
    }
}