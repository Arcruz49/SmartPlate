namespace SmartPlate.Application.DTOs.Request;

public class UserMealsRuleRequest
{
    public string MealName { get; set; } = string.Empty;
    public string MealDescription { get; set; } = string.Empty;
    public DateTime MealDate { get; set; } = DateTime.Now;
    public TimeSpan MealTime { get; set; } = DateTime.Now.TimeOfDay;
    public int Calories { get; set; }
    public decimal ProteinG { get; set; }
    public decimal CarbsG { get; set; }
    public decimal FatG { get; set; }
}