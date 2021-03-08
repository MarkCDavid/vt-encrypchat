using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using vt_encrypchat.Application.Operations.Exceptions;

namespace vt_encrypchat.Infrastructure.Configuration
{
    public static class ErrorHandling
    {
        public static void AddErrorHandling(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseExceptionHandler(ConstructHandler);
        }

        private static void ConstructHandler(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Run(HandleContextError);
        }

        private static async Task HandleContextError(HttpContext context)
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature.Error;

            switch (exception)
            {
                case OperationException operationException:
                    await HandleOperationException(context, operationException);
                    break;
                default:
                    throw exception;
            }
        }

        private static async Task HandleOperationException(HttpContext context, OperationException operationException)
        {
            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(new { error = operationException.Message });
        }
    }
}