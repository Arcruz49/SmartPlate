namespace SmartPlate.Application.DTOs.Request;

public class UserMealsRequest
{
    public string MealName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public byte[] ImageBytes { get; set; } = null!;
}
