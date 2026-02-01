using SmartPlate.Infrastructure.AI.Gemini;

namespace SmartPlate.Application.Interfaces;
public interface IParseGeminiUserMealCase
{
    Task<MealData> ExecuteAsync(string rawJson);
}
