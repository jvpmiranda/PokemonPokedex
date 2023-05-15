using ApiPokedex.Contract;
using System.Text.Json;

namespace ApiPokedex.Middleware
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
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
                err.Message = $"Erro proposital: { ex.Message}";
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = err.Status;
                await context.Response.WriteAsync(err.ToString());
            }
        }
    }    

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static void UseExceptionHandlerMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
