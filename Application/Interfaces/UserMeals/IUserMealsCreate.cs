namespace SmartPlate.Application.Interfaces;

using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;
public interface IUserMealsCreate
{
    Task<UserMealsResponse> ExecuteAsync(Guid userId, UserMealsRequest request);
}