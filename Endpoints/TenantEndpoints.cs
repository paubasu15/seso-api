using SesoApi.Models;
using SesoApi.Services;

namespace SesoApi.Endpoints;

public static class TenantEndpoints
{
    public static void MapTenantEndpoints(this WebApplication app)
    {
        app.MapGet("/api/tenant/config", (string? tenant, TenantStore store) =>
        {
            if (string.IsNullOrWhiteSpace(tenant))
                return Results.BadRequest(new { error = "Query parameter 'tenant' is required." });

            var config = store.GetBySlug(tenant);
            if (config is null)
                return Results.NotFound(new { error = $"Tenant '{tenant}' not found." });

            return Results.Ok(config);
        })
        .WithName("GetTenantConfig")
        .WithTags("Tenant");

        app.MapPut("/api/tenant/config", (string? tenant, UpdateTenantRequest request, TenantStore store) =>
        {
            if (string.IsNullOrWhiteSpace(tenant))
                return Results.BadRequest(new { error = "Query parameter 'tenant' is required." });

            var updated = store.Update(tenant, request);
            if (!updated)
                return Results.NotFound(new { error = $"Tenant '{tenant}' not found." });

            var config = store.GetBySlug(tenant);
            return Results.Ok(config);
        })
        .WithName("UpdateTenantConfig")
        .WithTags("Tenant");

        app.MapGet("/api/tenant/modules", (string? tenant, TenantStore store) =>
        {
            if (string.IsNullOrWhiteSpace(tenant))
                return Results.BadRequest(new { error = "Query parameter 'tenant' is required." });

            var config = store.GetBySlug(tenant);
            if (config is null)
                return Results.NotFound(new { error = $"Tenant '{tenant}' not found." });

            return Results.Ok(new { modules = config.Modules });
        })
        .WithName("GetTenantModules")
        .WithTags("Tenant");

        app.MapGet("/api/tenants", (TenantStore store) =>
        {
            var tenants = store.GetAll().Select(t => new
            {
                t.Slug,
                t.Name,
                t.Logo,
                t.PrimaryColor
            });
            return Results.Ok(tenants);
        })
        .WithName("ListTenants")
        .WithTags("Tenant");
    }
}
