using FundAdministration.API.Extensions;
using FundAdministration.DTOs.Investors;
using FundAdministration.UseCases.Investors.Create;
using FundAdministration.UseCases.Investors.Delete;
using FundAdministration.UseCases.Investors.Get;
using FundAdministration.UseCases.Investors.List;
using FundAdministration.UseCases.Investors.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace FundAdministration.API.Controllers.Investors;

[ApiController]
[Route("[controller]")]
public class InvestorController : ControllerBase
{
    private readonly IMediator mediator;
    public InvestorController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<InvestorListDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAsync()
    {
        var result = await mediator.Send(new ListInvestorQuery());

        return result.ToActionResult();
    }

    [HttpGet("{guid:guid}")]
    [ProducesResponseType(typeof(CreateInvestorDataDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetInvestorAsync(Guid guid)
    {
        var result = await mediator.Send(new GetInvestorQuery(guid));

        return result.ToActionResult();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateInvestor(
            [FromBody] CreateInvestorRequest request)
    {
        var command = new CreateInvestorCommand(
            request.fullName,
            request.emailId,
            request.fundId
            );
        var result = await mediator.Send(command);

        return result.ToActionResult();
    }

    [HttpDelete("{guid:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteInvestor(Guid guid)
    {
        var command = new DeleteInvestorCommand(guid);
        var result = await mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPut("{guid:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateInvestor(Guid guid, [FromBody] UpdateInvestorRequest request)
    {
        var command = new UpdateInvestorCommand(
            guid,
            request.fullName,
            request.emailId,
            request.fundId
            );
        var result = await mediator.Send(command);

        return result.ToActionResult();
    }
}