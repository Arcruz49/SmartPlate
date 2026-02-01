namespace SmartPlate.Application.Interfaces;

public interface IUserMealsDelete
{
    Task ExecuteAsync(Guid mealId, Guid userId);
}