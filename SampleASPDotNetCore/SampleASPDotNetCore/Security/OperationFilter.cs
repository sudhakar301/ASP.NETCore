using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SampleASPDotNetCore.Security
{
    /// <summary>
    /// This class will be used for to add xsrftoken as a header in the swagger
    /// </summary>
    public class OperationFilter : IOperationFilter
    {

        public OperationFilter()
        {
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

            operation.Parameters ??= new List<OpenApiParameter>();

            if (context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any(i => i.AuthenticationSchemes == JwtBearerDefaults.AuthenticationScheme))
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "X-XSRF-TOKEN",
                    In = ParameterLocation.Header,
                    Description = "User Authority JWT Token",
                    Required = true
                });
            }

            //if (context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any(i => i.Policy == Constants.Policies.ApiKeyTokenPolicy))
            //{
            //    operation.Parameters.Add(new OpenApiParameter()
            //    {
            //        Name = "X-API-Key",
            //        In = ParameterLocation.Header,
            //        Description = "Item Manager API Key",
            //        Required = true
            //    });
            //}
        }
    }
}
