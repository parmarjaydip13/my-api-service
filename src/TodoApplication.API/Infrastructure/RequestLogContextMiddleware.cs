using Serilog.Context;

namespace TodoApplication.API.Infrastructure
{
    public class RequestLogContextMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLogContextMiddleware(RequestDelegate next) => _next = next;

        public Task InvokeAsync(HttpContext context)
        {
            using (LogContext.PushProperty("CorrelanationId", context.TraceIdentifier))
            {
                return _next(context);
            }
        }
    }
}
