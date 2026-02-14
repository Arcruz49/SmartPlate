using System.Text.Json;
using SmartPlate.Application.Interfaces;
using SmartPlate.Infrastructure.APIs.OpenFoodFacts;

namespace SmartPlate.Application.UseCases;

public class ParseOpenFoodFactsCase : IParseOpenFoodFactsCase
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public Task<OpenFoodResponse> ExecuteAsync(string rawJson)
    {
        var response = JsonSerializer.Deserialize<OpenFoodResponse>(rawJson, Options)
            ?? throw new InvalidOperationException("JSON inválido do OpenFoodFacts.");

        if (response.Product?.Nutriments == null)
            throw new InvalidOperationException("Produto não contém dados nutricionais.");

        return Task.FromResult(response);
    }
}
