using SmartPlate.Domain.Enums;

namespace SmartPlate.Application.DTOs.Request;

public class UserDataRequest
{
    public decimal WeightKg { get; set; }
    public decimal HeightCm { get; set; }
    public int Age { get; set; }
    public BiologicalSex BiologicalSex { get; set; }
    public int WorkoutsPerWeek { get; set; }
    public TrainingType TrainingType { get; set; }
    public TrainingIntensity TrainingIntensity { get; set; }
    public DailyActivityLevel DailyActivityLevel { get; set; }

    public UserGoals Goal { get; set; }
    public int? SleepQuality { get; set; }
    public int? StressLevel { get; set; }
    public int? RoutineConsistency { get; set; }
    public string WorkoutDetails { get; set; } = string.Empty;
    public string DailyActivityDetails { get; set; } = string.Empty;
}
