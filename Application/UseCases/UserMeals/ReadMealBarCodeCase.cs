using Microsoft.EntityFrameworkCore;
using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.Interfaces;
using SmartPlate.Domain.Entities;
using SmartPlate.Infrastructure.APIs.OpenFoodFacts;
using SmartPlate.Infrastructure.Data;


namespace SmartPlate.Application.UseCases;

public class ReadMealBarCodeCase : IReadMealBarCodeCase{

    private readonly IOpenFoodFactsClient _openFoodFactsClient;

    public ReadMealBarCodeCase(IOpenFoodFactsClient openFoodFactsClient)
    {
        _openFoodFactsClient = openFoodFactsClient;
    }
    public async Task<OpenFoodResponse> ExecuteAsync(UserMealBarCodeRequest request)
    {
        var openFoodResponseRawResponse = await _openFoodFactsClient.SendPromptAsync(request.Code);

        

    }
}
