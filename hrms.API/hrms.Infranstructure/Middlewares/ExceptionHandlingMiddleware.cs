using hrms.Infranstructure.Logging;
using hrms.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using static System.Formats.Asn1.AsnWriter;

namespace hrms.Infranstructure.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Handle the exception
                await logger.LogError(context.Request.Path, ex.Message, context.RequestAborted, ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // Set the response status code and return an error response
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var errorResponse = new ServiceResult<object>
            {
                Success = false,
                ErrorOccured = true,
                ErrorMessage = ex.Message,
            };

            var jsonErrorResponse = JsonConvert.SerializeObject(errorResponse);
            return context.Response.WriteAsync(jsonErrorResponse);
        }
    }
}
