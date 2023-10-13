using Microsoft.AspNetCore.Http;

namespace hrms.Application.Infranstructure.Middlewares
{
    public class UserAgentMiddleware
    {
        private readonly RequestDelegate _next;

        public UserAgentMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Retrieve User-Agent
            var userAgent = context.Request.Headers["User-Agent"].ToString();

            // Add it to HttpContext items so it can be used later within the same request.
            context.Items["UserAgent"] = userAgent;

            // Call the next delegate/middleware in the pipeline
            await _next(context).ConfigureAwait(false);
        }
    }
}
