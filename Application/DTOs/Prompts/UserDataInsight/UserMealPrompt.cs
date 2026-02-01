using SmartPlate.Application.DTOs.Responses;

namespace SmartPlate.Application.DTOs.Prompts;

public class UserMealPrompt
{
    public string system_instruction { get; set; } = string.Empty;
    public UserMealInput user_meal { get; set; } = null!;
    public ResponseFormatUserMeal response_format { get; set; } = new();
    public string instruction { get; set; } = string.Empty;
}

