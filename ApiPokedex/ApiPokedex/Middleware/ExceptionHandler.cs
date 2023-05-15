namespace ApiPokedex.Middleware
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                return next.Invoke(context);
            }
            catch (Exception ex)
            {

                return Task.FromException(ex);
            }
        }
    }
}
