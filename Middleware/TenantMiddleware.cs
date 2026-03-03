namespace SesoApi.Middleware;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Detect tenant from X-Tenant header or ?tenant= query parameter
        if (!context.Request.Query.ContainsKey("tenant"))
        {
            var headerTenant = context.Request.Headers["X-Tenant"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(headerTenant))
            {
                var queryString = context.Request.QueryString.HasValue
                    ? context.Request.QueryString.Value + "&tenant=" + Uri.EscapeDataString(headerTenant)
                    : "?tenant=" + Uri.EscapeDataString(headerTenant);

                context.Request.QueryString = new QueryString(queryString);
            }
        }

        await _next(context);
    }
}
