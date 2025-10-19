using FundAdministration.API.Extensions;
using FundAdministration.Common.Transactions;
using FundAdministration.UseCases.Transactions.List;
using FundAdministration.UseCases.Transactions.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace FundAdministration.API.Controllers.Investors;

/// <summary>
/// API Controller for managing transactions.
/// Provides endpoints to list transactions by investor, get total amounts grouped by fund, and register new transactions.
/// </summary>
[ApiController]
[Route("[controller]")]
[Authorize]
public class TransactionController : ControllerBase
{
    private readonly IMediator mediator;
    public TransactionController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Retrieves all transactions for a specific investor.
    /// </summary>
    /// <param name="investorId">The unique identifier of the investor.</param>
    /// <returns>A <see cref="TransactionListDTO"/> containing the investor's transactions if found; otherwise, a 404 Not Found response.</returns>
    [HttpGet("TransactionByInvestor/{investorId:guid}")]
    [ProducesResponseType(typeof(TransactionListDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTransactionByInvestorAsync(Guid investorId)
    {
        var result = await mediator.Send(new GetTransactionByInvestorQuery(investorId));

        return result.ToActionResult(this);
    }

    /// <summary>
    /// Retrieves the total transaction amounts grouped by fund.
    /// </summary>
    /// <returns>A <see cref="TransactionListDTO"/> containing totals grouped by fund.</returns>
    [HttpGet("TotalAmountGroupByFund")]
    [ProducesResponseType(typeof(TransactionListDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTotalAmountGroupByFundAsync()
    {
        var result = await mediator.Send(new GetTotalAmountGroupByFundQuery());

        return result.ToActionResult(this);
    }

    /// <summary>
    /// Registers a new transaction for an investor.
    /// </summary>
    /// <param name="request">The <see cref="RegisterTransactionRequest"/> containing transaction details.</param>
    /// <returns>A 201 Created response if successful; otherwise, a 400 Bad Request response.</returns>
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

        return result.ToActionResult(this);
    }

}