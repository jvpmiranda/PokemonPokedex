using ApiPokedex.Contract.v1.Out;

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
            var err = new Error();
            err.Status = 500;
            err.Message = $"Erro : { ex.Message}";
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = err.Status;
            GravarErro(ex);
            await context.Response.WriteAsync(err.ToString());
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
