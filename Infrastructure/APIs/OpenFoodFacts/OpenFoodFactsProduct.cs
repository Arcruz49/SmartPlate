using System.Text.Json.Serialization;

namespace SmartPlate.Infrastructure.APIs.OpenFoodFacts;

public class Product
{
    [JsonPropertyName("_id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("_keywords")]
    public List<string>? Keywords { get; set; }

    [JsonPropertyName("nutriments")]
    public Nutriments? Nutriments { get; set; }
}