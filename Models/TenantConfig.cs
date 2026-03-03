namespace SesoApi.Models;

public class TenantConfig
{
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;
    public string PrimaryColor { get; set; } = string.Empty;
    public string SecondaryColor { get; set; } = string.Empty;
    public string BackgroundColor { get; set; } = string.Empty;
    public string TextColor { get; set; } = string.Empty;
    public HeroConfig Hero { get; set; } = new();
    public List<FeatureConfig> Features { get; set; } = [];
    public FooterConfig Footer { get; set; } = new();
    public List<TenantModule> Modules { get; set; } = [];
}

public class HeroConfig
{
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string CtaText { get; set; } = string.Empty;
    public string CtaLink { get; set; } = string.Empty;
    public string BackgroundImage { get; set; } = string.Empty;
}

public class FeatureConfig
{
    public string Icon { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class FooterConfig
{
    public string Text { get; set; } = string.Empty;
    public List<FooterLink> Links { get; set; } = [];
}

public class FooterLink
{
    public string Label { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}
