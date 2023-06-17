namespace ApiPokedex.Middleware;

public class LoggerMiddleware : IMiddleware
{
    private readonly ILogger _logger;

    public LoggerMiddleware(ILogger<LoggerMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _logger.LogInformation("Request: {Path}", context.Request.Path);

        Stream originalBody = context.Response.Body;
        try
        {
            using (var memStream = new MemoryStream())
            {
                context.Response.Body = memStream;

                await next(context);

                memStream.Position = 0;
                string responseBody = new StreamReader(memStream).ReadToEnd();
                _logger.LogInformation("Response status code: {statusCode} - Response: {response}", context.Response.StatusCode, responseBody);

                memStream.Position = 0;
                await memStream.CopyToAsync(originalBody);
            }
        }
        finally
        {
            context.Response.Body = originalBody;
        }
    }
}

public static class LoggerMiddlewareExtensions
{
    public static void UseLoggerMiddleware(this WebApplication app)
    {
        app.UseMiddleware<LoggerMiddleware>();
    }
}
