using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace FundAdministration.API.Extensions;

public static  class ActionResultExtensions
{
    public static ActionResult ToActionResult<T>(this Result<T> result, ControllerBase controller)
    {
        if (result.IsSuccess)
        {
            return controller.Ok(result.Value);
        }
        return result.Status switch
        {
            ResultStatus.NotFound => controller.NotFound(CreateProblem(result, controller, 404, "Resouce not found")),
            ResultStatus.Invalid => controller.BadRequest(CreateValidationProblem(result, controller)),
            ResultStatus.Unauthorized => controller.Unauthorized(CreateProblem(result, controller, 401, "Unathorized")),
            ResultStatus.Forbidden => controller.Forbid()
        };
    }

    private static ProblemDetails CreateProblem<T>(Result<T> result, ControllerBase controller, int status, string title)
    {
        return new ProblemDetails
        {
            Type = $"https://httpstatuses.com/{status}",
            Title = title,
            Detail = result.Errors.FirstOrDefault(),
            Status = status,
            Instance = controller.HttpContext?.Request?.Path
        };
    }

    private static ValidationProblemDetails CreateValidationProblem<T>(Result<T> result, ControllerBase controller)
    {
        var errors = result.ValidationErrors
            ?.GroupBy(e => e.Identifier)
            .ToDictionary(g => g.Key ?? "General", g => g.Select(e => e.ErrorMessage).ToArray())
            ?? new Dictionary<string, string[]>
            {
                { "General", result.Errors.ToArray() }
            };

        return new ValidationProblemDetails(errors)
        {
            Type = "https://httpstatuses.com/400",
            Title = "Validation failed",
            Detail = "One or more validation errors occurred.",
            Status = 400,
            Instance = controller.HttpContext?.Request?.Path
        };
    }

}
