using SmartPlate.Application.DTOs.Request;

namespace SmartPlate.Application.Interfaces;

public interface IAMealPromptService
{
    Task<string> ExecuteAsync(UserMealsRequest userMeal);
}