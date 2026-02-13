using System.Text.Json.Serialization;

namespace SmartPlate.Infrastructure.APIs.OpenFoodFacts;

public class OpenFoodResponse
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("status_verbose")]
    public string StatusVerbose { get; set; } = string.Empty;

    [JsonPropertyName("product")]
    public Product? Product { get; set; }
}