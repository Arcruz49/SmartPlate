using SmartPlate.Infrastructure.AI.Gemini;

namespace SmartPlate.Application.Interfaces;
public interface IParseGeminiUserInsightsCase
{
    Task<NutritionInsight> ExecuteAsync(string rawJson);
}
