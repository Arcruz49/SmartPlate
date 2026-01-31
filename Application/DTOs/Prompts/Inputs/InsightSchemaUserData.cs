namespace SmartPlate.Application.DTOs.Prompts;

public class InsightSchemaUserData
{
    public string target_calories { get; set; } = "number (daily total)";
    public string protein_target_g { get; set; } = "number (total grams)";
    public string carbs_target_g { get; set; } = "number (total grams)";
    public string fat_target_g { get; set; } = "number (total grams)";
    public string sleep_hours_target { get; set; } = "number (recommended hours)";
}