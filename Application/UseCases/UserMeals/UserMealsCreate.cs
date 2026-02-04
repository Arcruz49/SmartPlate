using Microsoft.EntityFrameworkCore;
using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.Interfaces;
using SmartPlate.Domain.Entities;
using SmartPlate.Infrastructure.Data;


namespace SmartPlate.Application.UseCases;

public class UserMealsCreate : IUserMealsCreate{

    private readonly Context _db;
    private readonly IAMealPromptService _mealPromptService;
    private readonly IAIClient _aiClient;
    private readonly IParseGeminiUserMealCase _parseGeminiUserMeal;

    public UserMealsCreate(Context db, IAMealPromptService mealPromptService, IAIClient aiClient, IParseGeminiUserMealCase parseGeminiUserMealCase)
    {
        _db = db;
        _mealPromptService = mealPromptService;
        _aiClient = aiClient;
        _parseGeminiUserMeal = parseGeminiUserMealCase;
    }
    public async Task<UserMealsResponse> ExecuteAsync(Guid userId, UserMealsRequest request)
    {
        var user = await _db.Users.AsNoTracking().Where(a => a.Id == userId).AnyAsync();

        if(!user) throw new InvalidOperationException("Usuário não encontrado.");

        var prompt = await _mealPromptService.ExecuteAsync(request);

        var aiRawResponse = await _aiClient.SendPromptAsync(prompt, request.ImageBytes);

        var mealData = await _parseGeminiUserMeal.ExecuteAsync(aiRawResponse);

        var meal = await _parseGeminiUserMeal.ExecuteAsync(aiRawResponse);

        var newUserMeal = new UserMeal
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            MealName = request.MealName,
            MealDescription = request.Description,
            MealDate = DateTime.Now.Date,
            MealTime = DateTime.Now.TimeOfDay,
            Calories = Convert.ToInt32(meal.calories),
            ProteinG = Convert.ToInt32(meal.protein_g),
            CarbsG = Convert.ToInt32(meal.carbs_g),
            FatG = Convert.ToInt32(meal.fat_g),
            Explanation = meal.explanation,
            Advice = meal.advice,
        };

        _db.UserMeal.Add(newUserMeal);
        await _db.SaveChangesAsync();

        return new UserMealsResponse(
            newUserMeal.Id,
            newUserMeal.MealName,
            newUserMeal.MealDescription,
            newUserMeal.MealDate,
            newUserMeal.MealTime,
            newUserMeal.Calories,
            newUserMeal.ProteinG,
            newUserMeal.CarbsG,
            newUserMeal.FatG
        );

    }
}
