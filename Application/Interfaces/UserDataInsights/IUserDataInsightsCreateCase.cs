namespace SmartPlate.Application.Interfaces;

using SmartPlate.Application.DTOs.Responses;
public interface IUserDataInsightsCreateCase
{
    Task<UserDataResponse> ExecuteAsync(Guid userId);
}