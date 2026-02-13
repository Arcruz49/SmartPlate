using System.Text.Json.Serialization;

namespace SmartPlate.Infrastructure.APIs.OpenFoodFacts;

public class Nutriments
{
    public double? Carbohydrates { get; set; }

    [JsonPropertyName("energy-kcal")]
    public double? EnergyKcal { get; set; }

    public double? Fat { get; set; }
    public double? Proteins { get; set; }

}