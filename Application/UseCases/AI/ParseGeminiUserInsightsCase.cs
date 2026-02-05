using System.Text.Json;
using SmartPlate.Application.Interfaces;
using SmartPlate.Infrastructure.AI.Gemini;
using SmartPlate.Infrastructure.Data;


namespace SmartPlate.Application.UseCases;

public class ParseGeminiUserInsightsCase : IParseGeminiUserInsightsCase{
    private readonly Context _db;
    public ParseGeminiUserInsightsCase(Context db)
    {
        _db = db;
    }
    public Task<NutritionInsight> ExecuteAsync(string rawJson)
    {
        var fullResponse = JsonSerializer.Deserialize<GeminiResponse>(rawJson)
            ?? throw new InvalidOperationException("Resposta da IA inválida (JSON nulo).");

        var insightJson =
            fullResponse.candidates.FirstOrDefault()?.content?.parts?.FirstOrDefault()?.text
            ?? throw new InvalidOperationException("Resposta da IA não contém insight válido.");

        var insight = JsonSerializer.Deserialize<NutritionInsight>(insightJson)
            ?? throw new InvalidOperationException("Falha ao desserializar NutritionInsight.");

        return Task.FromResult(insight);
    }
}
