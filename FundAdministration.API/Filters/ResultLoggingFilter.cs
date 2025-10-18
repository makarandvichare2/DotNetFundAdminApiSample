using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FundAdministration.Common.Filters
{
    public class SeriLogErrorFilter : IActionFilter
    {
        private readonly ILogger<SeriLogErrorFilter> _logger;

        public SeriLogErrorFilter(ILogger<SeriLogErrorFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
            var objectResult = context.Result as ObjectResult;
            if (objectResult.Value is Result<object> && ((Result<object>)objectResult.Value).Status == ResultStatus.Error)
            {
                LogError(context, ((Result<object>)objectResult.Value).Errors);
            }
            else
            {
                LogError(context, ((Result)objectResult.Value).Errors);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            //nothing do
        }

        private void LogError(ActionExecutedContext context,IEnumerable<string> errorsList)
        {
            string actionName = context.ActionDescriptor.DisplayName;
            string errors = errorsList != null ? string.Join(", ", errorsList) : "No error details";
            _logger.LogError("Error occurred in action {Action}: {Errors}", actionName, errors);
        }
    }
}
