using Asp.Versioning;
using FundAdministration.API.Extensions;
using FundAdministration.Common.Investors;
using FundAdministration.UseCases.Investors.Create;
using FundAdministration.UseCases.Investors.Delete;
using FundAdministration.UseCases.Investors.Get;
using FundAdministration.UseCases.Investors.List;
using FundAdministration.UseCases.Investors.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace FundAdministration.API.Controllers.Investors;

/// <summary>
/// API Controller for managing investors.
/// Provides endpoints to list, get, create, update, and delete investors.
/// </summary>
[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
[ControllerName("Investor")]
[Authorize]
public class InvestorV1Controller : ControllerBase
{
    private readonly IMediator mediator;
    public InvestorV1Controller(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Retrieves a list of all investors.
    /// </summary>
    /// <returns>A list of <see cref="InvestorListDTO"/> investors.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<InvestorListDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAsync()
    {
        var result = await mediator.Send(new ListInvestorQuery());

        return result.ToActionResult(this);
    }

    /// <summary>
    /// Retrieves a specific investor by their unique identifier.
    /// </summary>
    /// <param name="guid">The unique identifier of the investor.</param>
    /// <returns>A <see cref="CreateInvestorDataDTO"/> representing the investor if found; otherwise, a 404 Not Found response.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CreateInvestorDataDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetInvestorAsync(Guid id)
    {
        var result = await mediator.Send(new GetInvestorQuery(id));

        return result.ToActionResult(this);
    }

    /// <summary>
    /// Creates a new investor.
    /// </summary>
    /// <param name="request">The <see cref="CreateInvestorRequest"/> containing investor details.</param>
    /// <returns>A 201 Created response if successful; otherwise, a 400 Bad Request response.</returns>
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

        return result.ToActionResult(this);
    }

    /// <summary>
    /// Deletes an existing investor by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the investor to delete.</param>
    /// <returns>A 204 No Content response if deleted successfully; otherwise, a 404 Not Found response.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteInvestor(Guid id)
    {
        var command = new DeleteInvestorCommand(id);
        var result = await mediator.Send(command);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Updates an existing investor.
    /// </summary>
    /// <param name="id">The unique identifier of the investor to update.</param>
    /// <param name="request">The <see cref="UpdateInvestorRequest"/> containing updated investor details.</param>
    /// <returns>A 204 No Content response if updated successfully; otherwise, a 400 Bad Request response.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateInvestor(Guid id, [FromBody] UpdateInvestorRequest request)
    {
        var command = new UpdateInvestorCommand(
            id,
            request.fullName,
            request.emailId,
            request.fundId
            );
        var result = await mediator.Send(command);

        return result.ToActionResult(this);
    }
}