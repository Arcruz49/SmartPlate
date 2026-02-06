namespace SmartPlate.Application.Interfaces;

using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.DTOs.Snapshots;

public interface IRegisterUserBodyMetricsCase
{
    Task ExecuteAsync(UserBodyMetricsSnapshot snapshot);
}