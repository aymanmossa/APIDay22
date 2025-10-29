using Azure.Core;

namespace APIDay22.Api.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly ILogger<LoggingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var start = DateTime.UtcNow;
            await _next(context);
            var duration = DateTime.UtcNow - start;

            _logger.LogInformation("Request [{method}] {path} took {time} ms",
                context.Request.Method,
                context.Request.Path,
                duration.TotalMicroseconds);
        }
    }

    public static class LoggingMiddlewareExtentions
    {
        public static IApplicationBuilder UserRequestLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoggingMiddleware>();
        }
    }
}
