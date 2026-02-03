namespace SmartPlate.Infrastructure.AI.Gemini;
public class MealData
{
    public decimal calories { get; set; }
    public decimal protein_g { get; set; }
    public decimal carbs_g { get; set; }
    public decimal fat_g { get; set; }
    public string explanation { get; set; } = string.Empty;
}