using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Expressions;
using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.Interfaces;
using SmartPlate.Domain.Entities;
using SmartPlate.Infrastructure.Data;


namespace SmartPlate.Application.UseCases;

public class UserMealsRuleCreate : IUserMealsRuleCreate{

    private readonly Context _db;

    public UserMealsRuleCreate(Context db)
    {
        _db = db;
    }
    public async Task<UserMealsResponse> ExecuteAsync(Guid userId, UserMealsRuleRequest request)
    {
        var user = await _db.Users.AsNoTracking().Where(a => a.Id == userId).AnyAsync();

        if(!user) throw new InvalidOperationException("Usuário não encontrado.");

        var newUserMeal = new UserMeal
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            MealName = request.MealName,
            MealDescription = request.MealDescription,
            MealDate = request.MealDate.Date,
            MealTime = request.MealDate.TimeOfDay,
            Calories = request.Calories,
            ProteinG = request.ProteinG,
            FatG = request.FatG,
            CarbsG = request.CarbsG,
            AiGenerated = false,
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
