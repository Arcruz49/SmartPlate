using SmartPlate.Application.DTOs.Prompts;
using SmartPlate.Application.DTOs.Request;

namespace SmartPlate.Application.Interfaces;

public interface IAIInsightsPromptService
{
    Task<UserDataInsightPrompt> ExecuteAsync(UserDataRequest userData);
}