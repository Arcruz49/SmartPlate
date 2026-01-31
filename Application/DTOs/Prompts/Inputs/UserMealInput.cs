namespace SmartPlate.Application.DTOs.Prompts;

public class UserMealInput
{
    public string meal_Name { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public byte[] image_bytes { get; set; } = null!;
}