using FundAdministration.API.Extensions;
using FundAdministration.Common.Transactions;
using FundAdministration.UseCases.Investors.Create;
using FundAdministration.UseCases.Transactions.List;
using FundAdministration.UseCases.Transactions.Register;
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


    [HttpGet("TransactionByInvestor/{guid:guid}")]
    [ProducesResponseType(typeof(TransactionListDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTransactionByInvestorAsync(Guid guid)
    {
        var result = await mediator.Send(new GetTransactionByInvestorQuery(guid));

        return result.ToActionResult();
    }

    [HttpGet("TotalAmountGroupByFund")]
    [ProducesResponseType(typeof(TransactionListDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTotalAmountGroupByFundAsync()
    {
        var result = await mediator.Send(new GetTotalAmountGroupByFundQuery());

        return result.ToActionResult();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterTransactions(
            [FromBody] RegisterTransactionRequest request)
    {
        var command = new RegisterTransactionCommand(
            request.transactionType,
            request.amount,
            request.transactionDate,
            request.investorId
            );
        var result = await mediator.Send(command);

        return result.ToActionResult();
    }

}