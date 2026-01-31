using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using SmartPlate.Application.Interfaces;
using SmartPlate.Infrastructure.AI.Gemini;

namespace SmartPlate.Infrastructure.AI;

public class AIClient : IAIClient
{
    private readonly HttpClient _httpClient;
    private readonly GeminiOptions _options;


    public AIClient(HttpClient httpClient,  IOptions<GeminiOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<string> SendPromptAsync(string prompt, byte[]? imageBytes = null)
    {
        var url = $"{_options.BaseUrl}?key={_options.ApiKey}";

        var parts = new List<object>
        {
            new { text = prompt }
        };

        if (imageBytes is not null)
        {
            parts.Add(new
            {
                inline_data = new
                {
                    mime_type = "image/jpeg",
                    data = Convert.ToBase64String(imageBytes)
                }
            });
        }

        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts
                }
            },
            generationConfig = new
            {
                response_mime_type = "application/json",
                temperature = 0.1
            }
        };

        var response = await _httpClient.PostAsJsonAsync(url, requestBody);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new InvalidOperationException(
                $"Erro na API Gemini: {(int)response.StatusCode} - {error}"
            );
        }

        return await response.Content.ReadAsStringAsync();
    }
}
