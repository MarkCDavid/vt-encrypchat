using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using vt_encrypchat.Application.Operations.Exceptions;
using vt_encrypchat.Presentation.WebModels;

namespace vt_encrypchat.Infrastructure.Configuration
{
    public static class ErrorHandling
    {
        public static void AddErrorHandling(this IApplicationBuilder applicationBuilder, bool development)
        {
            applicationBuilder.UseExceptionHandler(
                applicationError =>
                {
                    applicationError.Run(
                        ConstructContextErrorHandler(
                            development ? DefaultExceptionHandler_Development : DefaultExceptionHandler_Production
                        )
                    );
                });
        }

        private static RequestDelegate ConstructContextErrorHandler(
            ExceptionHandler defaultExceptionHandler)
        {
            return async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                switch (exception)
                {
                    case OperationException operationException:
                        await HandleOperationException(context, operationException);
                        break;
                    default:
                        await defaultExceptionHandler(context, exception);
                        break;
                }
            };
        }

        private static async Task HandleOperationException(HttpContext context, OperationException operationException)
        {
            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(new ErrorViewModel {Error = operationException.Message});
        }

        private static async Task DefaultExceptionHandler_Development(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(new ErrorViewModel
            {
                Error = "Server error occurred.",
                Exception = new ExceptionViewModel
                {
                    Message = exception.Message,
                    StackTrace = exception.StackTrace
                }
            });
        }

        private static async Task DefaultExceptionHandler_Production(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(new ErrorViewModel
            {
                Error = "Server error occurred.",
                Exception = new ExceptionViewModel
                {
                    Message = "We are investigating the issue."
                }
            });
        }

        private delegate Task ExceptionHandler(HttpContext context, Exception exception);
    }
}