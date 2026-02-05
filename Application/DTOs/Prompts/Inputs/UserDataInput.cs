namespace SmartPlate.Application.DTOs.Prompts;

public class UserDataInput
{
    public decimal weight_kg { get; set; }
    public decimal height_cm { get; set; }
    public int age { get; set; }
    public string biological_sex { get; set; } = string.Empty;
    public int workouts_per_week { get; set; }
    public string training_type { get; set; } = string.Empty;
    public string training_intensity { get; set; } = string.Empty;
    public string daily_activity_level { get; set; } = string.Empty;
    public string user_goal { get; set; } = string.Empty;
    public int? sleep_quality { get; set; }
    public int? stress_level { get; set; }
    public int? routine_consistency { get; set; }
    public string workout_details { get; set; } = string.Empty;
    public string daily_activity_details { get; set; } = string.Empty;
}