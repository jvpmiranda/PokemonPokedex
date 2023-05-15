using ApiPokedex.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiPokedex.Middleware
{
    public class LoggerMiddleware : IMiddleware
    {
        private readonly ILogger _logger;
        
        public LoggerMiddleware(ILogger<LoggerMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _logger.LogInformation("Requested parameters: {RequestBody}", context.Request.Body);
            await next.Invoke(context);
            _logger.LogInformation("Response: {Response}", context.Response.Body);
        }
    }    

    public static class LoggerMiddlewareExtensions
    {
        public static void UseLoggerMiddleware(this WebApplication app)
        {
            app.UseMiddleware<LoggerMiddleware>();
        }
    }
}
