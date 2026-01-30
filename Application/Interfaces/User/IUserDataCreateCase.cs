namespace SmartPlate.Application.Interfaces;

using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;
public interface IUserDataCreateCase
{
    Task<UserDataResponse> ExecuteAsync(Guid userId, UserDataRequest request);
}