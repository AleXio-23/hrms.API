using hrms.Application.Infranstructure.Interfaces;
using hrms.Persistance;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace hrms.Application.Infranstructure.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
            var dbContext = scope.ServiceProvider.GetRequiredService<HrmsAppDbContext>();

            var stopwatch = Stopwatch.StartNew();

            try
            {
                await _next(context).ConfigureAwait(false);
                // Log information after the request is processed
                await logger.LogInformationAsync(context.Request.Path, "Request processed successfully", context.RequestAborted).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Log the error if an exception occurs
                await logger.LogError(context.Request.Path, ex.Message, context.RequestAborted, ex).ConfigureAwait(false);
                throw;
            }
            finally
            {
                // Log the elapsed time
                stopwatch.Stop();
                await logger.LogInformationAsync(context.Request.Path, $"Request took: {stopwatch.ElapsedMilliseconds} ms", context.RequestAborted).ConfigureAwait(false);
            }
        }
    }
}
