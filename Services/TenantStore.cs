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
            Hero = new HeroConfig
            {
                Title = "Bienvenido a SESO",
                Subtitle = "La plataforma SaaS multi-tenant para tu negocio",
                CtaText = "Comenzar ahora",
                CtaLink = "/app",
                BackgroundImage = ""
            },
            Features =
            [
                new FeatureConfig { Icon = "rocket", Title = "Rápido", Description = "Despliegue en minutos" },
                new FeatureConfig { Icon = "shield", Title = "Seguro", Description = "Protección total de datos" },
                new FeatureConfig { Icon = "chart", Title = "Analytics", Description = "Datos en tiempo real" }
            ],
            Footer = new FooterConfig
            {
                Text = "© 2026 SESO. Todos los derechos reservados.",
                Links =
                [
                    new FooterLink { Label = "Términos", Url = "/terms" },
                    new FooterLink { Label = "Privacidad", Url = "/privacy" }
                ]
            },
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
            Hero = new HeroConfig
            {
                Title = "Bienvenido a Acme",
                Subtitle = "La mejor solución para tu negocio",
                CtaText = "Comenzar ahora",
                CtaLink = "/app",
                BackgroundImage = ""
            },
            Features =
            [
                new FeatureConfig { Icon = "rocket", Title = "Rápido", Description = "Velocidad increíble" },
                new FeatureConfig { Icon = "shield", Title = "Seguro", Description = "Protección total" },
                new FeatureConfig { Icon = "chart", Title = "Analytics", Description = "Datos en tiempo real" }
            ],
            Footer = new FooterConfig
            {
                Text = "© 2026 Acme Corp. Todos los derechos reservados.",
                Links =
                [
                    new FooterLink { Label = "Términos", Url = "/terms" },
                    new FooterLink { Label = "Privacidad", Url = "/privacy" }
                ]
            },
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
            Hero = new HeroConfig
            {
                Title = "Bienvenido a Globex",
                Subtitle = "Soluciones globales para empresas locales",
                CtaText = "Explorar",
                CtaLink = "/app",
                BackgroundImage = ""
            },
            Features =
            [
                new FeatureConfig { Icon = "globe", Title = "Global", Description = "Presencia mundial" },
                new FeatureConfig { Icon = "shield", Title = "Confiable", Description = "Años de experiencia" },
                new FeatureConfig { Icon = "chart", Title = "Crecimiento", Description = "Escala con tu negocio" }
            ],
            Footer = new FooterConfig
            {
                Text = "© 2026 Globex. Todos los derechos reservados.",
                Links =
                [
                    new FooterLink { Label = "Términos", Url = "/terms" },
                    new FooterLink { Label = "Privacidad", Url = "/privacy" },
                    new FooterLink { Label = "Contacto", Url = "/contact" }
                ]
            },
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
            if (request.Hero is not null) existing.Hero = request.Hero;
            if (request.Features is not null) existing.Features = request.Features;
            if (request.Footer is not null) existing.Footer = request.Footer;
        }

        return true;
    }
}
