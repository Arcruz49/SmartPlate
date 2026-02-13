using SmartPlate.Infrastructure.APIs.OpenFoodFacts;

namespace SmartPlate.Application.Interfaces;
public interface IParseOpenFoodFactsCase
{
    Task<OpenFoodResponse> ExecuteAsync(string rawJson);
}
