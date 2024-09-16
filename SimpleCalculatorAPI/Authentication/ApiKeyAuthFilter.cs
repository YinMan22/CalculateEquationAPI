using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SimpleCalculatorAPI.Authentication
{
    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _config;

        public ApiKeyAuthFilter(IConfiguration config)
        {
            _config = config;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var requestApiKey))
            {
                context.Result = new UnauthorizedObjectResult("API Key is missing.");
                return;
            }

            var apiKey = _config.GetValue<string>(AuthConstants.ApiKeySectionName);
            if (!apiKey.Equals(requestApiKey))
            {
                context.Result = new UnauthorizedObjectResult("Invalid API Key.");
                return;
            }
        }
    }
}
