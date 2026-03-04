using System.Collections.Concurrent;
using SesoApi.Models;

namespace SesoApi.Services;

public class TenantStore
{
    private readonly ConcurrentDictionary<string, TenantConfig> _published = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, TenantConfig> _drafts = new(StringComparer.OrdinalIgnoreCase);
    private readonly object _updateLock = new();

    // Template catalog: each template lists its component types in order with default data
    private static readonly Dictionary<string, List<TenantComponent>> TemplateDefaults = new(StringComparer.OrdinalIgnoreCase)
    {
        ["default"] =
        [
            new TenantComponent { Type = "header", Order = 1, Visible = true, Data = new Dictionary<string, object> { ["logo"] = "" } },
            new TenantComponent { Type = "hero", Order = 2, Visible = true, Data = new Dictionary<string, object> { ["title"] = "Bienvenido", ["subtitle"] = "La plataforma SaaS multi-tenant para tu negocio", ["ctaText"] = "Comenzar ahora", ["ctaLink"] = "/app", ["backgroundImage"] = "" } },
            new TenantComponent { Type = "features", Order = 3, Visible = true, Data = new Dictionary<string, object> { ["items"] = new List<object> { new Dictionary<string, object> { ["icon"] = "rocket", ["title"] = "Rápido", ["description"] = "Despliegue en minutos" }, new Dictionary<string, object> { ["icon"] = "shield", ["title"] = "Seguro", ["description"] = "Protección total de datos" }, new Dictionary<string, object> { ["icon"] = "chart", ["title"] = "Analytics", ["description"] = "Datos en tiempo real" } } } },
            new TenantComponent { Type = "footer", Order = 10, Visible = true, Data = new Dictionary<string, object> { ["text"] = "© 2026. Todos los derechos reservados.", ["links"] = new List<object> { new Dictionary<string, object> { ["label"] = "Términos", ["url"] = "/terms" }, new Dictionary<string, object> { ["label"] = "Privacidad", ["url"] = "/privacy" } } } }
        ],
        ["corporate"] =
        [
            new TenantComponent { Type = "header", Order = 1, Visible = true, Data = new Dictionary<string, object> { ["logo"] = "" } },
            new TenantComponent { Type = "hero", Order = 2, Visible = true, Data = new Dictionary<string, object> { ["title"] = "Bienvenido", ["subtitle"] = "La mejor solución para tu negocio", ["ctaText"] = "Comenzar ahora", ["ctaLink"] = "/app", ["backgroundImage"] = "" } },
            new TenantComponent { Type = "features", Order = 3, Visible = true, Data = new Dictionary<string, object> { ["items"] = new List<object> { new Dictionary<string, object> { ["icon"] = "rocket", ["title"] = "Rápido", ["description"] = "Velocidad increíble" }, new Dictionary<string, object> { ["icon"] = "shield", ["title"] = "Seguro", ["description"] = "Protección total" }, new Dictionary<string, object> { ["icon"] = "chart", ["title"] = "Analytics", ["description"] = "Datos en tiempo real" } } } },
            new TenantComponent { Type = "footer", Order = 10, Visible = true, Data = new Dictionary<string, object> { ["text"] = "© 2026. Todos los derechos reservados.", ["links"] = new List<object> { new Dictionary<string, object> { ["label"] = "Términos", ["url"] = "/terms" }, new Dictionary<string, object> { ["label"] = "Privacidad", ["url"] = "/privacy" } } } }
        ],
        ["nature"] =
        [
            new TenantComponent { Type = "header", Order = 1, Visible = true, Data = new Dictionary<string, object> { ["logo"] = "" } },
            new TenantComponent { Type = "hero", Order = 2, Visible = true, Data = new Dictionary<string, object> { ["title"] = "Bienvenido", ["subtitle"] = "Soluciones globales para empresas locales", ["ctaText"] = "Explorar", ["ctaLink"] = "/app", ["backgroundImage"] = "" } },
            new TenantComponent { Type = "features", Order = 3, Visible = true, Data = new Dictionary<string, object> { ["items"] = new List<object> { new Dictionary<string, object> { ["icon"] = "globe", ["title"] = "Global", ["description"] = "Presencia mundial" }, new Dictionary<string, object> { ["icon"] = "shield", ["title"] = "Confiable", ["description"] = "Años de experiencia" }, new Dictionary<string, object> { ["icon"] = "chart", ["title"] = "Crecimiento", ["description"] = "Escala con tu negocio" } } } },
            new TenantComponent { Type = "testimonials", Order = 4, Visible = false, Data = new Dictionary<string, object> { ["title"] = "Lo que dicen nuestros clientes", ["items"] = new List<object> { new Dictionary<string, object> { ["name"] = "Cliente Ejemplo", ["role"] = "CEO, Empresa", ["text"] = "Excelente servicio y atención." } } } },
            new TenantComponent { Type = "pricing", Order = 5, Visible = false, Data = new Dictionary<string, object> { ["title"] = "Planes y precios", ["items"] = new List<object>() } },
            new TenantComponent { Type = "footer", Order = 10, Visible = true, Data = new Dictionary<string, object> { ["text"] = "© 2026. Todos los derechos reservados.", ["links"] = new List<object> { new Dictionary<string, object> { ["label"] = "Términos", ["url"] = "/terms" }, new Dictionary<string, object> { ["label"] = "Privacidad", ["url"] = "/privacy" }, new Dictionary<string, object> { ["label"] = "Contacto", ["url"] = "/contact" } } } }
        ],
        ["starter"] =
        [
            new TenantComponent { Type = "header", Order = 1, Visible = true, Data = new Dictionary<string, object> { ["logo"] = "" } },
            new TenantComponent { Type = "hero", Order = 2, Visible = true, Data = new Dictionary<string, object> { ["title"] = "Bienvenido", ["subtitle"] = "Tu negocio en línea", ["ctaText"] = "Empezar", ["ctaLink"] = "/app", ["backgroundImage"] = "" } },
            new TenantComponent { Type = "features", Order = 3, Visible = true, Data = new Dictionary<string, object> { ["items"] = new List<object>() } },
            new TenantComponent { Type = "footer", Order = 10, Visible = true, Data = new Dictionary<string, object> { ["text"] = "© 2026. Todos los derechos reservados.", ["links"] = new List<object>() } }
        ],
        ["business"] =
        [
            new TenantComponent { Type = "header", Order = 1, Visible = true, Data = new Dictionary<string, object> { ["logo"] = "" } },
            new TenantComponent { Type = "hero", Order = 2, Visible = true, Data = new Dictionary<string, object> { ["title"] = "Bienvenido", ["subtitle"] = "Tu negocio en línea", ["ctaText"] = "Empezar", ["ctaLink"] = "/app", ["backgroundImage"] = "" } },
            new TenantComponent { Type = "features", Order = 3, Visible = true, Data = new Dictionary<string, object> { ["items"] = new List<object>() } },
            new TenantComponent { Type = "testimonials", Order = 4, Visible = true, Data = new Dictionary<string, object> { ["title"] = "Lo que dicen nuestros clientes", ["items"] = new List<object> { new Dictionary<string, object> { ["name"] = "Cliente Ejemplo", ["role"] = "CEO, Empresa", ["text"] = "Excelente servicio y atención." } } } },
            new TenantComponent { Type = "pricing", Order = 5, Visible = true, Data = new Dictionary<string, object> { ["title"] = "Planes y precios", ["items"] = new List<object>() } },
            new TenantComponent { Type = "footer", Order = 10, Visible = true, Data = new Dictionary<string, object> { ["text"] = "© 2026. Todos los derechos reservados.", ["links"] = new List<object>() } }
        ]
    };

    public TenantStore()
    {
        Seed();
    }

    private void Seed()
    {
        _published["default"] = new TenantConfig
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

        _published["acme"] = new TenantConfig
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

        _published["globex"] = new TenantConfig
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

    // GET config: if draft=true and draft exists, return draft. Otherwise return published.
    public TenantConfig? Get(string slug, bool draft = false)
    {
        if (draft && _drafts.TryGetValue(slug, out var draftConfig))
        {
            var result = CloneConfig(draftConfig);
            result.HasDraft = true;
            return result;
        }
        if (!_published.TryGetValue(slug, out var config))
            return null;
            
        var published = CloneConfig(config);
        published.HasDraft = _drafts.ContainsKey(slug);
        return published;
    }

    public TenantConfig? GetBySlug(string slug) => Get(slug);

    public IEnumerable<TenantConfig> GetAll() => _published.Values.Select(c =>
    {
        var clone = CloneConfig(c);
        clone.HasDraft = _drafts.ContainsKey(c.Slug);
        return clone;
    });

    // PUT config: always saves as draft
    public TenantConfig? SaveDraft(string slug, UpdateTenantRequest request)
    {
        if (!_published.TryGetValue(slug, out var published))
            return null;

        lock (_updateLock)
        {
            // Start from existing draft or published
            var baseConfig = _drafts.TryGetValue(slug, out var existingDraft)
                ? CloneConfig(existingDraft)
                : CloneConfig(published);

            ApplyRequest(baseConfig, request);
            _drafts[slug] = baseConfig;
        }

        return Get(slug, draft: true);
    }

    // POST publish: copy draft to published, remove draft
    public TenantConfig? Publish(string slug)
    {
        if (!_published.ContainsKey(slug))
            return null;

        lock (_updateLock)
        {
            if (_drafts.TryRemove(slug, out var draft))
            {
                draft.HasDraft = false;
                _published[slug] = draft;
            }
        }

        return Get(slug);
    }

    // DELETE draft: discard the draft
    public bool DiscardDraft(string slug)
    {
        return _drafts.TryRemove(slug, out _);
    }

    // PUT config with publish=true: save and publish directly
    public TenantConfig? SaveAndPublish(string slug, UpdateTenantRequest request)
    {
        if (!_published.TryGetValue(slug, out var existing))
            return null;

        lock (_updateLock)
        {
            var baseConfig = CloneConfig(existing);
            ApplyRequest(baseConfig, request);
            baseConfig.HasDraft = false;
            _published[slug] = baseConfig;
            _drafts.TryRemove(slug, out _);
        }

        return Get(slug);
    }

    public bool HasDraftFor(string slug) => _drafts.ContainsKey(slug);

    // Kept for backward compatibility
    public bool Update(string slug, UpdateTenantRequest request) =>
        SaveDraft(slug, request) is not null;

    private void ApplyRequest(TenantConfig config, UpdateTenantRequest request)
    {
        if (request.Name is not null) config.Name = request.Name;
        if (request.Logo is not null) config.Logo = request.Logo;
        if (request.PrimaryColor is not null) config.PrimaryColor = request.PrimaryColor;
        if (request.SecondaryColor is not null) config.SecondaryColor = request.SecondaryColor;
        if (request.BackgroundColor is not null) config.BackgroundColor = request.BackgroundColor;
        if (request.TextColor is not null) config.TextColor = request.TextColor;

        if (request.Template is not null && request.Template != config.Template)
        {
            // Template change: merge components
            config.Template = request.Template;
            config.Components = MergeComponentsForTemplate(request.Template, config.Components);
        }
        else if (request.Components is not null)
        {
            // Merge by type
            var existingByType = config.Components.ToDictionary(c => c.Type, StringComparer.OrdinalIgnoreCase);
            foreach (var incoming in request.Components)
            {
                existingByType[incoming.Type] = incoming;
            }
            config.Components = existingByType.Values.OrderBy(c => c.Order).ToList();
        }
    }

    private static List<TenantComponent> MergeComponentsForTemplate(string templateName, List<TenantComponent> existing)
    {
        if (!TemplateDefaults.TryGetValue(templateName, out var templateComponents))
            return existing;

        var existingByType = existing.ToDictionary(c => c.Type, StringComparer.OrdinalIgnoreCase);
        var merged = new List<TenantComponent>();

        foreach (var templateComp in templateComponents)
        {
            if (existingByType.TryGetValue(templateComp.Type, out var existingComp))
            {
                // Keep existing data, but update order from template
                merged.Add(new TenantComponent
                {
                    Type = existingComp.Type,
                    Order = templateComp.Order,
                    Visible = existingComp.Visible,
                    Data = existingComp.Data
                });
            }
            else
            {
                // Add with template defaults
                merged.Add(new TenantComponent
                {
                    Type = templateComp.Type,
                    Order = templateComp.Order,
                    Visible = templateComp.Visible,
                    Data = new Dictionary<string, object>(templateComp.Data)
                });
            }
        }

        return merged.OrderBy(c => c.Order).ToList();
    }

    private static TenantConfig CloneConfig(TenantConfig source) => new()
    {
        Slug = source.Slug,
        Name = source.Name,
        Logo = source.Logo,
        PrimaryColor = source.PrimaryColor,
        SecondaryColor = source.SecondaryColor,
        BackgroundColor = source.BackgroundColor,
        TextColor = source.TextColor,
        Template = source.Template,
        Components = source.Components.Select(c => new TenantComponent
        {
            Type = c.Type,
            Order = c.Order,
            Visible = c.Visible,
            Data = new Dictionary<string, object>(c.Data)
        }).ToList(),
        Modules = source.Modules.Select(m => new TenantModule
        {
            Id = m.Id,
            Name = m.Name,
            Icon = m.Icon,
            Active = m.Active
        }).ToList(),
        HasDraft = source.HasDraft
    };
}
