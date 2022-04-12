using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace UserService.Attributes
{
    public class MicroserviceAuthAttribute : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyHeaderName = "ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api key not provided"

                };
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var apiKey = configuration.GetValue<string>("APIkey:Key");

            if (!apiKey.Equals(potentialApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api key not provided"

                };
                return;
            }
            await next();
        }
    }
}
