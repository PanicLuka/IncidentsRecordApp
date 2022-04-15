using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;


namespace UserService
{
    public class AddCommonParameOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "apiKey",
                In = ParameterLocation.Header,
                Description = "Api Key",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                }
            });
        }
    }
}
