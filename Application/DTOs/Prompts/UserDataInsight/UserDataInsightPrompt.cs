using SmartPlate.Application.DTOs.Responses;

namespace SmartPlate.Application.DTOs.Prompts;

public class UserDataInsightPrompt
{
    public string system_instruction { get; set; } = string.Empty;
    public UserDataInput user_data { get; set; } = null!;
    public ResponseFormat response_format { get; set; } = new();
    public string instruction { get; set; } = string.Empty;
}

