using Microsoft.AspNetCore.Diagnostics;
using SampleASPDotNetCore.Exceptions;

namespace SampleASPDotNetCore
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetailsService;

        public GlobalExceptionHandler(IProblemDetailsService problemDetailsService)
        {
            _problemDetailsService = problemDetailsService;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = "application/json";
            var excDetails = exception switch
            {
                MValidationAppException => (Detail: exception.Message, StatusCode: StatusCodes.Status422UnprocessableEntity),
                _ => (Detail: exception.Message, StatusCode: StatusCodes.Status500InternalServerError)
            };
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            if (exception is MValidationAppException validationException)
            {
                await httpContext.Response.WriteAsJsonAsync(new { validationException.errors });
                return true;
            }
            //If not a MValidationAppException exceptiontype, we will write a generic error response
            return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails =
                {
                    Title = "An error occurred while processing your request.",
                    Detail = excDetails.Detail,
                    Type=exception.GetType().Name,
                    Status = excDetails.StatusCode
                },
                Exception = exception
            });
            

        }

    }
}
