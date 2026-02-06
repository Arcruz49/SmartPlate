namespace SmartPlate.Application.Interfaces;

using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;
public interface IGetUserMetricsCase
{
    Task<List<UserBodyMetricsResponse>> ExecuteAsync(Guid userId);
}