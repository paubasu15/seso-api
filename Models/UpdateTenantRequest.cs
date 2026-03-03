namespace SesoApi.Models;

public class UpdateTenantRequest
{
    public string? Name { get; set; }
    public string? Logo { get; set; }
    public string? PrimaryColor { get; set; }
    public string? SecondaryColor { get; set; }
    public string? BackgroundColor { get; set; }
    public string? TextColor { get; set; }
    public HeroConfig? Hero { get; set; }
    public List<FeatureConfig>? Features { get; set; }
    public FooterConfig? Footer { get; set; }
}
