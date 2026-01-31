using SmartPlate.Domain.Enums;

namespace SmartPlate.Application.DTOs.Responses;

public record UserDataInsightsResponse(
    decimal target_calories,
    decimal protein_target_g,
    decimal carbs_target_g,
    decimal fat_target_g,
    decimal sleep_hours_target
);
