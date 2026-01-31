namespace SmartPlate.Application.Interfaces;
public interface IAIClient
{
    Task<string> SendPromptAsync(string prompt, byte[]? imageBytes = null);
}
