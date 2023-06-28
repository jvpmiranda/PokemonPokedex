using ApiPokedex.Contract;
using System.Text.Json;

namespace ApiPokedex.Middleware;

public class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger _logger;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {   
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            var err = new ErrorStatus() { Status = new Status() };
            err.Status.HttpStatusCode = 500;
            err.Status.Message = $"Erro : { ex.Message}";
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = err.Status.HttpStatusCode;
            GravarErro(ex);
            await context.Response.WriteAsync(JsonSerializer.Serialize(err));
        }
    }

    private void GravarErro(Exception e)
    {
        _logger.LogInformation("Erro durante a execução: {erro}", e.Message);
        _logger.LogInformation("Stack: {stack}", e.StackTrace ?? string.Empty);
        if (e.InnerException != null)
            GravarErro(e.InnerException);
    }
}    

public static class ExceptionHandlerMiddlewareExtensions
{
    public static void UseExceptionHandlerMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
