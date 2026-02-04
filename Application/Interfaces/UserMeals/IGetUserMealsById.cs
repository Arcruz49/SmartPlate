using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;

namespace SmartPlate.Application.Interfaces;

public interface IGetUserMealsById
{
    Task<UserMealByIDResponse> ExecuteAsync(Guid userId, UserMealsIdRequest request);
}