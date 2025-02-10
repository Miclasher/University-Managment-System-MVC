namespace University.UI.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.Redirect($"/{context.Request.RouteValues["controller"]}/Index?errorMessage={exception.Message}");
            return Task.CompletedTask;
        }
    }
}
