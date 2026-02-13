using SmartPlate.Infrastructure.APIs.OpenFoodFacts;

namespace SmartPlate.Application.Interfaces;
public interface ParseOpenFoodFactsCase
{
    Task<OpenFoodResponse> ExecuteAsync(string rawJson);
}
