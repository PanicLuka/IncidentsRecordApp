using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserService.Attributes
{
    public class MicroserviceAuthAttribute : ActionFilterAttribute
    {
        private const string ApiKeyValue = "";

        public MicroserviceAuthAttribute() :base() {
            
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var ApiKeyValue = configuration.GetValue<string>("APIkey:Key");
            var headers = context.HttpContext.Request.Headers
;
            if (headers["apiKey"] == ApiKeyValue)
            {
                return;
            }

            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "Api key not provided"

            };
        }
    }

  
}
