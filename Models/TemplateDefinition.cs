namespace SesoApi.Models;

public class TemplateDefinition
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Thumbnail { get; set; } = string.Empty;
    public List<ComponentDefinition> Components { get; set; } = [];
}

public class ComponentDefinition
{
    public string Type { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool Visible { get; set; } = true;
    public List<FieldSchema> Schema { get; set; } = [];
    public Dictionary<string, object> DefaultData { get; set; } = [];
}

public class FieldSchema
{
    public string Key { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // "text", "textarea", "color", "select", "list", "image", "url", "toggle", "number"
    public string Label { get; set; } = string.Empty;
    public string? Placeholder { get; set; }
    public bool Required { get; set; }
    public List<string>? Options { get; set; } // For "select" type
    public List<FieldSchema>? ItemFields { get; set; } // For "list" type - defines the schema of each item in the list
}
