using SmartPlate.Domain.Enums;

namespace SmartPlate.Application.DTOs.Responses;

public record UserDataResponse(
    decimal WeightKg,
    decimal HeightCm,
    int Age,
    BiologicalSex BiologicalSex,
    int WorkoutsPerWeek,
    TrainingType TrainingType,
    TrainingIntensity TrainingIntensity,
    DailyActivityLevel DailyActivityLevel,
    UserGoals Goal,
    int? SleepQuality,
    int? StressLevel,
    int? RoutineConsistency,
    string WorkoutDetails,
    string DailyActivityDetails
);