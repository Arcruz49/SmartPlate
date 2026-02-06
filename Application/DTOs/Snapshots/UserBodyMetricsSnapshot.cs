namespace SmartPlate.Application.DTOs.Snapshots;
public record UserBodyMetricsSnapshot(
    Guid UserId,
    decimal WeightKg,
    decimal? BodyFatPercent,
    decimal? MuscleMassKg,
    decimal? WaistCm,
    decimal? ChestCm,
    DateTime MetricDate
);
