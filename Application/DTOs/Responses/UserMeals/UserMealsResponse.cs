namespace SmartPlate.Application.DTOs.Responses;

public record UserMealsResponse(
    DateTime meal_date,
    TimeSpan meal_time,
    decimal calories,
    decimal protein_g,
    decimal carbs_g,
    decimal fat_g
);