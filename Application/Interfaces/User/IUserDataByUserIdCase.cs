namespace SmartPlate.Application.Interfaces;

using SmartPlate.Application.DTOs.Responses;
public interface IUserDataByUserIdCase
{
    Task<UserDataResponse> ExecuteAsync(Guid userId);
}