using SmartPlate.Application.DTOs.Prompts;
using SmartPlate.Application.DTOs.Responses;

namespace SmartPlate.Application.Interfaces;

public interface IAIInsightsPromptService
{
    Task<string> ExecuteAsync(UserDataResponse userData);
}