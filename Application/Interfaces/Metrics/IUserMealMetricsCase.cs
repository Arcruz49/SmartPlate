namespace SmartPlate.Application.Interfaces;

using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;
public interface IUserMealMetricsCase
{
    Task<List<UserMealsMetricsResponse>> ExecuteAsync(Guid userId, UserMealsMetricsRequest request);
}