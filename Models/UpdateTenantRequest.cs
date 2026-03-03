namespace SesoApi.Models;

public class UpdateTenantRequest
{
    public string? Name { get; set; }
    public string? Logo { get; set; }
    public string? PrimaryColor { get; set; }
    public string? SecondaryColor { get; set; }
    public string? BackgroundColor { get; set; }
    public string? TextColor { get; set; }
    public string? Template { get; set; }
    public List<TenantComponent>? Components { get; set; }
}
