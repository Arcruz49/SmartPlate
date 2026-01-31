namespace SmartPlate.Application.Interfaces;

using SmartPlate.Application.DTOs.Responses;
public interface IUserDataInsightsCreateCase
{
    Task<UserDataInsightsResponse> ExecuteAsync(Guid userId);
}