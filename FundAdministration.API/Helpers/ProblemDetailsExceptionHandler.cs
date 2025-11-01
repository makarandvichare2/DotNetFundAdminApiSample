using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FundAdministration.API.Helpers
{
    public class ProblemDetailsExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ProblemDetailsExceptionHandler> _logger;

        public ProblemDetailsExceptionHandler(ILogger<ProblemDetailsExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception.Message);

            var statusCode = (int)HttpStatusCode.InternalServerError;
            var problemDetails = new ProblemDetails
            {
                Type = "https://httpstatuses.com/500",
                Title = "Internal Server Error",
                Status = statusCode,
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            };
            problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

            httpContext.Response.ContentType = "application/problem+json";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails);
            //var json = JsonSerializer.Serialize(problemDetails);
            //httpContext.Response.WriteAsync(json);
            return true;
        }
    }
}
