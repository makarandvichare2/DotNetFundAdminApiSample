using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            if(objectResult.StatusCode == StatusCodes.Status500InternalServerError)
            {
                LogError(context, (string[])objectResult.Value);
                SetGenericErrorMessage(objectResult);
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

        private void SetGenericErrorMessage(ObjectResult objectResult)
        {
            objectResult.Value = "Error occured";
        }
    }
}
