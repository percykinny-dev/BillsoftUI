namespace BS.UI.Web.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {

        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            HandleException(httpContext, ex);
        }
    }

    private void HandleException(HttpContext context, Exception exception)
    {
        try
        {
            var logService = context.RequestServices.GetService<ISysLogService>();
            logService.Log("mas-exceptionhandlingmiddleware", "error", exception);
            //context.Response.Redirect("/sy/dashboard/error", false);
            return;
        }
        catch
        {
            //suppress
        }
    }
}

