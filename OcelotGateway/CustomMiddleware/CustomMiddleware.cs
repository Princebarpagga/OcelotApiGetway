public class CustomMiddleware
{
    private readonly RequestDelegate _next;

    public CustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }


    public async Task InvokeAsync(HttpContext context)
    {
        // Log request details
        Console.WriteLine($"Request Method: {context.Request.Method}");
        Console.WriteLine($"Request Path: {context.Request.Path}");
        Console.WriteLine($"Query String: {context.Request.QueryString}");

        // Check for custom header
        if (!context.Request.Headers.TryGetValue("X-Custom-Header", out var customHeaderValue) || customHeaderValue != "MySecretValue")
        {
            // Return 403 Forbidden if the header is missing or invalid
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Forbidden: Missing or invalid custom header.");
            return;
        }

        // Call the next middleware in the pipeline
        await _next(context);

        // Custom logic after the next middleware is called
        Console.WriteLine($"Response Status Code: {context.Response.StatusCode}");
    }
}
