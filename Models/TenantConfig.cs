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
    public string Template { get; set; } = string.Empty;
    public List<TenantComponent> Components { get; set; } = [];
    public List<TenantModule> Modules { get; set; } = [];
    public bool HasDraft { get; set; }
}

public class TenantComponent
{
    public string Type { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool Visible { get; set; } = true;
    public Dictionary<string, object> Data { get; set; } = [];
}
