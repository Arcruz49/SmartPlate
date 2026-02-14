namespace SmartPlate.Application.Interfaces;
public interface IOpenFoodFactsClient
{
    Task<string> SendPromptAsync(string code);
}
