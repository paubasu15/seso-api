using SesoApi.Models;
using SesoApi.Services;

namespace SesoApi.Endpoints;

public static class TenantEndpoints
{
    public static void MapTenantEndpoints(this WebApplication app)
    {
        app.MapGet("/api/tenant/config", (string? tenant, bool? draft, TenantStore store) =>
        {
            if (string.IsNullOrWhiteSpace(tenant))
                return Results.BadRequest(new { error = "Query parameter 'tenant' is required." });

            var config = store.Get(tenant, draft == true);
            if (config is null)
                return Results.NotFound(new { error = $"Tenant '{tenant}' not found." });

            return Results.Ok(config);
        })
        .WithName("GetTenantConfig")
        .WithTags("Tenant");

        app.MapPut("/api/tenant/config", (string? tenant, bool? publish, UpdateTenantRequest request, TenantStore store) =>
        {
            if (string.IsNullOrWhiteSpace(tenant))
                return Results.BadRequest(new { error = "Query parameter 'tenant' is required." });

            TenantConfig? config;
            if (publish == true)
            {
                config = store.SaveAndPublish(tenant, request);
            }
            else
            {
                config = store.SaveDraft(tenant, request);
            }

            if (config is null)
                return Results.NotFound(new { error = $"Tenant '{tenant}' not found." });

            return Results.Ok(config);
        })
        .WithName("UpdateTenantConfig")
        .WithTags("Tenant");

        app.MapPost("/api/tenant/config/publish", (string? tenant, TenantStore store) =>
        {
            if (string.IsNullOrWhiteSpace(tenant))
                return Results.BadRequest(new { error = "Query parameter 'tenant' is required." });

            var config = store.Publish(tenant);
            if (config is null)
                return Results.NotFound(new { error = $"Tenant '{tenant}' not found." });

            return Results.Ok(config);
        })
        .WithName("PublishTenantDraft")
        .WithTags("Tenant");

        app.MapDelete("/api/tenant/config/draft", (string? tenant, TenantStore store) =>
        {
            if (string.IsNullOrWhiteSpace(tenant))
                return Results.BadRequest(new { error = "Query parameter 'tenant' is required." });

            var discarded = store.DiscardDraft(tenant);
            if (!discarded)
                return Results.NotFound(new { error = $"No draft found for tenant '{tenant}'." });

            return Results.Ok(new { message = $"Draft for tenant '{tenant}' discarded." });
        })
        .WithName("DiscardTenantDraft")
        .WithTags("Tenant");

        app.MapGet("/api/tenant/config/has-draft", (string? tenant, TenantStore store) =>
        {
            if (string.IsNullOrWhiteSpace(tenant))
                return Results.BadRequest(new { error = "Query parameter 'tenant' is required." });

            return Results.Ok(new { hasDraft = store.HasDraftFor(tenant) });
        })
        .WithName("HasTenantDraft")
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
                t.PrimaryColor,
                t.HasDraft
            });
            return Results.Ok(tenants);
        })
        .WithName("ListTenants")
        .WithTags("Tenant");
    }
}
