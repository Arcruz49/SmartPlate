namespace SmartPlate.Application.DTOs.Prompts;

public class InsightSchemaUserMeal
{
    public string calories { get; set; } = "number (total)";
    public string protein_g { get; set; } = "number (total grams)";
    public string carbs_g { get; set; } = "number (total grams)";
    public string fat_g { get; set; } = "number (total grams)";
}