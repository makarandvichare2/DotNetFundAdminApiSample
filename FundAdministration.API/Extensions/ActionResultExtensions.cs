using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace FundAdministration.API.Extensions;

public static  class ActionResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        return result.Status switch
        {
            ResultStatus.Ok => new OkObjectResult(result.Value),
            ResultStatus.Created => new CreatedResult(string.Empty, result.Value),
            ResultStatus.NotFound => new NotFoundResult(),
            ResultStatus.Invalid => new BadRequestObjectResult(result.ValidationErrors),
            ResultStatus.Unauthorized => new UnauthorizedResult(),
            ResultStatus.Forbidden => new ForbidResult(),
            ResultStatus.Error => new ObjectResult(result.Errors)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            },
            _ => new ObjectResult(result)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            }
        };
    }
}
