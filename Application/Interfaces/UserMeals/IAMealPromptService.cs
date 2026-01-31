using SmartPlate.Application.DTOs.Prompts;

namespace SmartPlate.Application.Interfaces;

public interface IAMealPromptService
{
    Task<string> ExecuteAsync(UserMealInput userMeal);
}