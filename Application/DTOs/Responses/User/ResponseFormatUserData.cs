using SmartPlate.Application.DTOs.Prompts;

namespace SmartPlate.Application.DTOs.Responses;
public class ResponseFormatUserData
{
    public string type { get; set; } = "json_object";
    public InsightSchemaUserData schema { get; set; } = new();
}