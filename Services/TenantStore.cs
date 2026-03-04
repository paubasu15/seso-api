using System.Collections.Concurrent;
using SesoApi.Models;

namespace SesoApi.Services;

public class TenantStore
{
    private readonly ConcurrentDictionary<string, TenantConfig> _published = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, TenantConfig> _drafts = new(StringComparer.OrdinalIgnoreCase);
    private readonly object _updateLock = new();
    private readonly TemplateRegistry _templateRegistry;

    public TenantStore(TemplateRegistry templateRegistry)
    {
        _templateRegistry = templateRegistry;
        Seed();
    }

    private void Seed()
    {
        var defaultModules = new List<TenantModule>
        {
            new TenantModule { Id = "landing-editor", Name = "Editor de Landing", Icon = "edit", Active = true },
            new TenantModule { Id = "analytics", Name = "Analytics", Icon = "chart", Active = true },
            new TenantModule { Id = "crm", Name = "CRM", Icon = "users", Active = true },
            new TenantModule { Id = "email-marketing", Name = "Email Marketing", Icon = "mail", Active = false }
        };

        // demo → starter template
        _published["demo"] = new TenantConfig
        {
            Slug = "demo",
            Name = "SESO Demo",
            Logo = "/images/seso-logo.png",
            PrimaryColor = "#DC2626",
            SecondaryColor = "#991B1B",
            BackgroundColor = "#0F0F0F",
            TextColor = "#F8FAFC",
            Template = "starter",
            Components = _templateRegistry.GetDefaultComponents("starter"),
            Modules = defaultModules.Select(m => new TenantModule { Id = m.Id, Name = m.Name, Icon = m.Icon, Active = m.Active }).ToList()
        };

        // acme → business template
        var acmeComponents = _templateRegistry.GetDefaultComponents("business");
        var acmeHeader = acmeComponents.FirstOrDefault(c => c.Type == "header");
        if (acmeHeader is not null) acmeHeader.Data["logo"] = "/images/acme-logo.png";

        _published["acme"] = new TenantConfig
        {
            Slug = "acme",
            Name = "Acme Corp",
            Logo = "/images/acme-logo.png",
            PrimaryColor = "#3B82F6",
            SecondaryColor = "#1E40AF",
            BackgroundColor = "#F8FAFC",
            TextColor = "#1E293B",
            Template = "business",
            Components = acmeComponents,
            Modules = defaultModules.Select(m => new TenantModule { Id = m.Id, Name = m.Name, Icon = m.Icon, Active = m.Id != "crm" && m.Id != "email-marketing" }).ToList()
        };

        // creative → portfolio template
        _published["creative"] = new TenantConfig
        {
            Slug = "creative",
            Name = "Creative Studio",
            Logo = "/images/creative-logo.png",
            PrimaryColor = "#8B5CF6",
            SecondaryColor = "#6D28D9",
            BackgroundColor = "#1E1B4B",
            TextColor = "#EDE9FE",
            Template = "portfolio",
            Components = _templateRegistry.GetDefaultComponents("portfolio"),
            Modules = defaultModules.Select(m => new TenantModule { Id = m.Id, Name = m.Name, Icon = m.Icon, Active = m.Id is "landing-editor" or "analytics" }).ToList()
        };

        // shop → ecommerce template
        _published["shop"] = new TenantConfig
        {
            Slug = "shop",
            Name = "My Shop",
            Logo = "/images/shop-logo.png",
            PrimaryColor = "#F59E0B",
            SecondaryColor = "#D97706",
            BackgroundColor = "#FFFBEB",
            TextColor = "#1C1917",
            Template = "ecommerce",
            Components = _templateRegistry.GetDefaultComponents("ecommerce"),
            Modules = defaultModules.Select(m => new TenantModule { Id = m.Id, Name = m.Name, Icon = m.Icon, Active = true }).ToList()
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

    private List<TenantComponent> MergeComponentsForTemplate(string templateName, List<TenantComponent> existing)
    {
        var templateComponents = _templateRegistry.GetDefaultComponents(templateName);
        if (templateComponents.Count == 0)
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
