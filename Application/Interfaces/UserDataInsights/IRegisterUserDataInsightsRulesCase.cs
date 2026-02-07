namespace SmartPlate.Application.Interfaces;

using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;
public interface IRegisterUserDataInsightsRulesCase
{
    Task<UserDataInsightsResponse> ExecuteAsync(Guid userId, UserDataInsightRequest userDataInsightRequest);
}