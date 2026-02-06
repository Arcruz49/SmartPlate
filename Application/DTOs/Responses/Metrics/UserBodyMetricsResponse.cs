namespace SmartPlate.Application.DTOs.Responses;
public record UserBodyMetricsResponse(
    // Guid UserId,
    decimal WeightKg,
    // decimal? BodyFatPercent,
    // decimal? MuscleMassKg,
    // decimal? WaistCm,
    // decimal? ChestCm,
    DateTime? MetricDate
);
