using SmartPlate.Application.DTOs.Request;
using SmartPlate.Infrastructure.APIs.OpenFoodFacts;

namespace SmartPlate.Application.Interfaces;

public interface IReadMealBarCodeCase
{
    Task<OpenFoodResponse> ExecuteAsync(UserMealBarCodeRequest request);
}