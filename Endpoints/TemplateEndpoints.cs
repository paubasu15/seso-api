using SesoApi.Services;

namespace SesoApi.Endpoints;

public static class TemplateEndpoints
{
    public static void MapTemplateEndpoints(this WebApplication app)
    {
        app.MapGet("/api/templates", (TemplateRegistry registry) =>
        {
            return Results.Ok(registry.GetAll());
        })
        .WithName("ListTemplates")
        .WithTags("Templates");

        app.MapGet("/api/templates/{id}", (string id, TemplateRegistry registry) =>
        {
            var template = registry.GetById(id);
            if (template is null)
                return Results.NotFound(new { error = $"Template '{id}' not found." });

            return Results.Ok(template);
        })
        .WithName("GetTemplate")
        .WithTags("Templates");
    }
}
