namespace SmartPlate.Application.Interfaces;

using SmartPlate.Application.DTOs.Responses;
public interface IUserDataInsightsByUserIdCase
{
    Task<UserDataInsightsResponse> ExecuteAsync(Guid userId);
}