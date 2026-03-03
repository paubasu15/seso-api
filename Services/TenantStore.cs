using System.Collections.Concurrent;
using SesoApi.Models;

namespace SesoApi.Services;

public class TenantStore
{
    private readonly ConcurrentDictionary<string, TenantConfig> _store = new(StringComparer.OrdinalIgnoreCase);
    private readonly object _updateLock = new();

    public TenantStore()
    {
        Seed();
    }

    private void Seed()
    {
        _store["default"] = new TenantConfig
        {
            Slug = "default",
            Name = "SESO",
            Logo = "/images/seso-logo.png",
            PrimaryColor = "#DC2626",
            SecondaryColor = "#991B1B",
            BackgroundColor = "#0F0F0F",
            TextColor = "#F8FAFC",
            Template = "default",
            Components =
            [
                new TenantComponent
                {
                    Type = "header",
                    Order = 1,
                    Visible = true,
                    Data = new Dictionary<string, object> { ["logo"] = "/images/seso-logo.png" }
                },
                new TenantComponent
                {
                    Type = "hero",
                    Order = 2,
                    Visible = true,
                    Data = new Dictionary<string, object>
                    {
                        ["title"] = "Bienvenido a SESO",
                        ["subtitle"] = "La plataforma SaaS multi-tenant para tu negocio",
                        ["ctaText"] = "Comenzar ahora",
                        ["ctaLink"] = "/app",
                        ["backgroundImage"] = ""
                    }
                },
                new TenantComponent
                {
                    Type = "features",
                    Order = 3,
                    Visible = true,
                    Data = new Dictionary<string, object>
                    {
                        ["items"] = new[]
                        {
                            new Dictionary<string, object> { ["icon"] = "rocket", ["title"] = "Rápido", ["description"] = "Despliegue en minutos" },
                            new Dictionary<string, object> { ["icon"] = "shield", ["title"] = "Seguro", ["description"] = "Protección total de datos" },
                            new Dictionary<string, object> { ["icon"] = "chart", ["title"] = "Analytics", ["description"] = "Datos en tiempo real" }
                        }
                    }
                },
                new TenantComponent
                {
                    Type = "footer",
                    Order = 10,
                    Visible = true,
                    Data = new Dictionary<string, object>
                    {
                        ["text"] = "© 2026 SESO. Todos los derechos reservados.",
                        ["links"] = new[]
                        {
                            new Dictionary<string, object> { ["label"] = "Términos", ["url"] = "/terms" },
                            new Dictionary<string, object> { ["label"] = "Privacidad", ["url"] = "/privacy" }
                        }
                    }
                }
            ],
            Modules =
            [
                new TenantModule { Id = "landing-editor", Name = "Editor de Landing", Icon = "edit", Active = true },
                new TenantModule { Id = "analytics", Name = "Analytics", Icon = "chart", Active = true },
                new TenantModule { Id = "crm", Name = "CRM", Icon = "users", Active = true },
                new TenantModule { Id = "email-marketing", Name = "Email Marketing", Icon = "mail", Active = false }
            ]
        };

        _store["acme"] = new TenantConfig
        {
            Slug = "acme",
            Name = "Acme Corp",
            Logo = "/images/acme-logo.png",
            PrimaryColor = "#3B82F6",
            SecondaryColor = "#1E40AF",
            BackgroundColor = "#F8FAFC",
            TextColor = "#1E293B",
            Template = "corporate",
            Components =
            [
                new TenantComponent
                {
                    Type = "header",
                    Order = 1,
                    Visible = true,
                    Data = new Dictionary<string, object> { ["logo"] = "/images/acme-logo.png" }
                },
                new TenantComponent
                {
                    Type = "hero",
                    Order = 2,
                    Visible = true,
                    Data = new Dictionary<string, object>
                    {
                        ["title"] = "Bienvenido a Acme",
                        ["subtitle"] = "La mejor solución para tu negocio",
                        ["ctaText"] = "Comenzar ahora",
                        ["ctaLink"] = "/app",
                        ["backgroundImage"] = ""
                    }
                },
                new TenantComponent
                {
                    Type = "features",
                    Order = 3,
                    Visible = true,
                    Data = new Dictionary<string, object>
                    {
                        ["items"] = new[]
                        {
                            new Dictionary<string, object> { ["icon"] = "rocket", ["title"] = "Rápido", ["description"] = "Velocidad increíble" },
                            new Dictionary<string, object> { ["icon"] = "shield", ["title"] = "Seguro", ["description"] = "Protección total" },
                            new Dictionary<string, object> { ["icon"] = "chart", ["title"] = "Analytics", ["description"] = "Datos en tiempo real" }
                        }
                    }
                },
                new TenantComponent
                {
                    Type = "footer",
                    Order = 10,
                    Visible = true,
                    Data = new Dictionary<string, object>
                    {
                        ["text"] = "© 2026 Acme Corp. Todos los derechos reservados.",
                        ["links"] = new[]
                        {
                            new Dictionary<string, object> { ["label"] = "Términos", ["url"] = "/terms" },
                            new Dictionary<string, object> { ["label"] = "Privacidad", ["url"] = "/privacy" }
                        }
                    }
                }
            ],
            Modules =
            [
                new TenantModule { Id = "landing-editor", Name = "Editor de Landing", Icon = "edit", Active = true },
                new TenantModule { Id = "analytics", Name = "Analytics", Icon = "chart", Active = true },
                new TenantModule { Id = "crm", Name = "CRM", Icon = "users", Active = false },
                new TenantModule { Id = "email-marketing", Name = "Email Marketing", Icon = "mail", Active = false }
            ]
        };

        _store["globex"] = new TenantConfig
        {
            Slug = "globex",
            Name = "Globex",
            Logo = "/images/globex-logo.png",
            PrimaryColor = "#16A34A",
            SecondaryColor = "#15803D",
            BackgroundColor = "#F0FDF4",
            TextColor = "#14532D",
            Template = "nature",
            Components =
            [
                new TenantComponent
                {
                    Type = "header",
                    Order = 1,
                    Visible = true,
                    Data = new Dictionary<string, object> { ["logo"] = "/images/globex-logo.png" }
                },
                new TenantComponent
                {
                    Type = "hero",
                    Order = 2,
                    Visible = true,
                    Data = new Dictionary<string, object>
                    {
                        ["title"] = "Bienvenido a Globex",
                        ["subtitle"] = "Soluciones globales para empresas locales",
                        ["ctaText"] = "Explorar",
                        ["ctaLink"] = "/app",
                        ["backgroundImage"] = ""
                    }
                },
                new TenantComponent
                {
                    Type = "features",
                    Order = 3,
                    Visible = true,
                    Data = new Dictionary<string, object>
                    {
                        ["items"] = new[]
                        {
                            new Dictionary<string, object> { ["icon"] = "globe", ["title"] = "Global", ["description"] = "Presencia mundial" },
                            new Dictionary<string, object> { ["icon"] = "shield", ["title"] = "Confiable", ["description"] = "Años de experiencia" },
                            new Dictionary<string, object> { ["icon"] = "chart", ["title"] = "Crecimiento", ["description"] = "Escala con tu negocio" }
                        }
                    }
                },
                new TenantComponent { Type = "testimonials", Order = 4, Visible = false, Data = [] },
                new TenantComponent { Type = "pricing", Order = 5, Visible = false, Data = [] },
                new TenantComponent
                {
                    Type = "footer",
                    Order = 10,
                    Visible = true,
                    Data = new Dictionary<string, object>
                    {
                        ["text"] = "© 2026 Globex. Todos los derechos reservados.",
                        ["links"] = new[]
                        {
                            new Dictionary<string, object> { ["label"] = "Términos", ["url"] = "/terms" },
                            new Dictionary<string, object> { ["label"] = "Privacidad", ["url"] = "/privacy" },
                            new Dictionary<string, object> { ["label"] = "Contacto", ["url"] = "/contact" }
                        }
                    }
                }
            ],
            Modules =
            [
                new TenantModule { Id = "landing-editor", Name = "Editor de Landing", Icon = "edit", Active = true },
                new TenantModule { Id = "analytics", Name = "Analytics", Icon = "chart", Active = true },
                new TenantModule { Id = "crm", Name = "CRM", Icon = "users", Active = true },
                new TenantModule { Id = "email-marketing", Name = "Email Marketing", Icon = "mail", Active = true }
            ]
        };
    }

    public TenantConfig? GetBySlug(string slug) =>
        _store.TryGetValue(slug, out var config) ? config : null;

    public IEnumerable<TenantConfig> GetAll() => _store.Values;

    public bool Update(string slug, UpdateTenantRequest request)
    {
        if (!_store.TryGetValue(slug, out var existing))
            return false;

        lock (_updateLock)
        {
            if (request.Name is not null) existing.Name = request.Name;
            if (request.Logo is not null) existing.Logo = request.Logo;
            if (request.PrimaryColor is not null) existing.PrimaryColor = request.PrimaryColor;
            if (request.SecondaryColor is not null) existing.SecondaryColor = request.SecondaryColor;
            if (request.BackgroundColor is not null) existing.BackgroundColor = request.BackgroundColor;
            if (request.TextColor is not null) existing.TextColor = request.TextColor;
            if (request.Template is not null) existing.Template = request.Template;
            if (request.Components is not null) existing.Components = request.Components;
        }

        return true;
    }
}
