using System.Security.Claims;

public class LabelBasedRoutingMiddleware
{
    private readonly RequestDelegate _next;

    public LabelBasedRoutingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Get user's label or role from context (example: assuming stored in Claims)
        var userLabel = context.User.FindFirst(ClaimTypes.Role)?.Value;

        // Determine downstream path based on user's label or role
        var downstreamPath = DetermineDownstreamPath(userLabel);

        // Update the request path if necessary
        if (!string.IsNullOrEmpty(downstreamPath))
        {
            context.Request.Path = new PathString(downstreamPath);
        }

        // Call the next middleware in the pipeline
        await _next(context);
    }

    private string DetermineDownstreamPath(string userLabel)
    {
        
        if (userLabel == "admin")
        {
            return "/Product/getProducts";
        }
        else
        {
            return "/products";
        }
    }
}
