namespace SmartPlate.Application.DTOs.Responses;

public record UserMealsMetricsResponse(
    DateTime meal_date,
    decimal calories_total,
    decimal protein_g_total,
    decimal carbs_g_total,
    decimal fat_g_total
);