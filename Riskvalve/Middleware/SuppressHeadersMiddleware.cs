public class SuppressHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public SuppressHeadersMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Response.OnStarting(() =>
        {
            if (!context.Response.Headers.ContainsKey("X-Content-Type-Options"))
            {
                context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
            }
            context.Response.Headers.Remove("X-Powered-By");
            context.Response.Headers.Remove("Server");
            return Task.CompletedTask;
        });

        await _next(context);
    }
}
