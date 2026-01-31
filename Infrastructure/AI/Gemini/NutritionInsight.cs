namespace SmartPlate.Infrastructure.AI.Gemini;
public class NutritionInsight
{
    public decimal target_calories { get; set; }
    public decimal protein_target_g { get; set; }
    public decimal carbs_target_g { get; set; }
    public decimal fat_target_g { get; set; }
    public decimal sleep_hours_target { get; set; }
}