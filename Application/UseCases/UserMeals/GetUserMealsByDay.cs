using Microsoft.EntityFrameworkCore;
using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.Interfaces;
using SmartPlate.Domain.Entities;
using SmartPlate.Infrastructure.Data;


namespace SmartPlate.Application.UseCases;

public class GetUserMealsByDay : IGetUserMealsByDay{

    private readonly Context _db;

    public GetUserMealsByDay(Context db)
    {
        _db = db;
    }
    public async Task<List<UserMealsResponse>> ExecuteAsync(Guid userId, UserMealsDayRequest request)
    {
        var user = await _db.Users.AsNoTracking().Where(a => a.Id == userId).AnyAsync();

        if(!user) throw new InvalidOperationException("Usuário não encontrado.");

        var startOfDay = request.Date.Date;
        var endOfDay = startOfDay.AddDays(1);

        var result = await _db.UserMeal
        .AsNoTracking()
        .Where(a => a.UserId == userId && a.MealDate >= startOfDay && a.MealDate < endOfDay)
        .Select(a => new UserMealsResponse(
            a.Id,
            a.MealName,
            a.MealDescription,
            a.MealDate,
            a.MealTime,
            a.Calories,
            a.ProteinG,
            a.CarbsG,
            a.FatG
        )).ToListAsync();

        return result;
    }
}
