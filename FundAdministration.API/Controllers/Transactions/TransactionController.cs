using FundAdministration.API.Extensions;
using FundAdministration.Common.Investors;
using FundAdministration.UseCases.Investors.Create;
using FundAdministration.UseCases.Transactions.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace FundAdministration.API.Controllers.Investors;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly IMediator mediator;
    public TransactionController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    [HttpGet("{guid:guid}")]
    [ProducesResponseType(typeof(CreateInvestorDataDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTransactionByInvestorAsync(Guid guid)
    {
        var result = await mediator.Send(new GetTransactionByInvestorQuery(guid));

        return result.ToActionResult();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterTransactions(
            [FromBody] RegisterTransactionRequest request)
    {
        var command = new CreateInvestorCommand(
            request.fullName,
            request.emailId,
            request.fundId
            );
        var result = await mediator.Send(command);

        return result.ToActionResult();
    }

}