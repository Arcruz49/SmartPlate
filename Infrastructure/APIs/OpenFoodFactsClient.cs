using Microsoft.Extensions.Options;
using SmartPlate.Application.Interfaces;
using SmartPlate.Infrastructure.APIs.OpenFoodFacts;

namespace SmartPlate.Infrastructure.APIs;

public class OpenFoodFactsClient : IOpenFoodFactsClient
{
    private readonly HttpClient _httpClient;
    private readonly OpenFoodFactsOptions _options;


    public OpenFoodFactsClient(HttpClient httpClient,  IOptions<OpenFoodFactsOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<string> SendPromptAsync(string code)
    {
        var url = $"{_options.BaseUrl}/{code}";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new InvalidOperationException(
                $"Erro na API: {(int)response.StatusCode} - {error}"
            );
        }

        return await response.Content.ReadAsStringAsync();
    }
}
