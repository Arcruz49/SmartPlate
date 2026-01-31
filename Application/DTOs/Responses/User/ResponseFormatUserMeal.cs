using SmartPlate.Application.DTOs.Prompts;

namespace SmartPlate.Application.DTOs.Responses;
public class ResponseFormatUserMeal
{
    public string type { get; set; } = "json_object";
    public InsightSchemaUserMeal schema { get; set; } = new();
}