using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiCatalogoNet8.Filters
{
    public class ApiLoggingFilter : IActionFilter
    {

        private readonly ILogger<ApiLoggingFilter> _logger;

        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            _logger = logger;
        }
        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"#### Executanto -> OnActionExecuting ####");
            _logger.LogInformation($"###########################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"ModelState: {context.ModelState.IsValid}");
            _logger.LogInformation($"###########################################");


        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"#### Executanto -> OnActionExecuted ####");
            _logger.LogInformation($"###########################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"StatusCode: {context.HttpContext.Response.StatusCode}");
            _logger.LogInformation($"###########################################");
        }

    }
}
