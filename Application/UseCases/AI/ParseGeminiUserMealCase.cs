using System.Text.Json;
using SmartPlate.Application.Interfaces;
using SmartPlate.Infrastructure.AI.Gemini;


namespace SmartPlate.Application.UseCases;

public class ParseGeminiUserMealCase : IParseGeminiUserMealCase{
    public ParseGeminiUserMealCase()
    {
    }
    public Task<MealData> ExecuteAsync(string rawJson)
    {
        var fullResponse = JsonSerializer.Deserialize<GeminiResponse>(rawJson)
            ?? throw new InvalidOperationException("Resposta da IA inválida (JSON nulo).");

        var mealDataJson =
            fullResponse.candidates.FirstOrDefault()?.content?.parts?.FirstOrDefault()?.text
            ?? throw new InvalidOperationException("Resposta da IA não contém dados do alimento válidos.");

        var mealData = JsonSerializer.Deserialize<MealData>(mealDataJson)
            ?? throw new InvalidOperationException("Falha ao desserializar NutritionInsight.");

        return Task.FromResult(mealData);
    }
}
