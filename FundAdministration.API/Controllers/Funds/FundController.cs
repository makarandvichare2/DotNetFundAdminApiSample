using FundAdministration.API.Extensions;
using FundAdministration.Common.Funds;
using FundAdministration.UseCases.Funds.Create;
using FundAdministration.UseCases.Funds.Delete;
using FundAdministration.UseCases.Funds.Get;
using FundAdministration.UseCases.Funds.List;
using FundAdministration.UseCases.Funds.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace FundAdministration.API.Controllers.Funds;

/// <summary>
/// API Controller for managing funds.
/// Provides endpoints to list, get, create, update, and delete funds.
/// </summary>
[ApiController]
[Route("[controller]")]
[Authorize]
public class FundController : ControllerBase
{
    private readonly IMediator mediator;
    public FundController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Retrieves a list of all funds.
    /// </summary>
    /// <returns>A list of <see cref="FundListDTO"/> funds.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<FundListDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAsync()
    {
        var result = await mediator.Send(new ListFundQuery());

        return result.ToActionResult(this);
    }

    /// <summary>
    /// Retrieves a specific fund by its unique identifier.
    /// </summary>
    /// <param name="guid">The unique identifier of the fund.</param>
    /// <returns>A <see cref="CreateFundDataDTO"/> representing the fund if found; otherwise, a 404 Not Found response.</returns>
    [HttpGet("{guid:guid}")]
    [ProducesResponseType(typeof(CreateFundDataDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFundAsync(Guid guid)
    {
        var result = await mediator.Send(new GetFundQuery(guid));

        return result.ToActionResult(this);
    }

    /// <summary>
    /// Creates a new fund.
    /// </summary>
    /// <param name="request">The <see cref="CreateFundRequest"/> containing fund details.</param>
    /// <returns>A 201 Created response if successful; otherwise, a 400 Bad Request response.</returns>
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

        return result.ToActionResult(this);
    }

    /// <summary>
    /// Deletes an existing fund by its unique identifier.
    /// </summary>
    /// <param name="guid">The unique identifier of the fund to delete.</param>
    /// <returns>A 204 No Content response if deleted successfully; otherwise, a 404 Not Found response.</returns>
    [HttpDelete("{guid:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFund(Guid guid)
    {
        var command = new DeleteFundCommand(guid);
        var result = await mediator.Send(command);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Updates an existing fund.
    /// </summary>
    /// <param name="guid">The unique identifier of the fund to update.</param>
    /// <param name="request">The <see cref="UpdateFundRequest"/> containing updated fund details.</param>
    /// <returns>A 204 No Content response if updated successfully; otherwise, a 400 Bad Request response.</returns>
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

        return result.ToActionResult(this);
    }
}