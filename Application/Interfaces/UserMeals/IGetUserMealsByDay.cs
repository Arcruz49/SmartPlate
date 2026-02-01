using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;

namespace SmartPlate.Application.Interfaces;

public interface IGetUserMealsByDay
{
    Task<List<UserMealsResponse>> ExecuteAsync(Guid userId, UserMealsDayRequest request);
}