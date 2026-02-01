using Microsoft.EntityFrameworkCore;
using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.Interfaces;
using SmartPlate.Domain.Entities;
using SmartPlate.Infrastructure.Data;


namespace SmartPlate.Application.UseCases;

public class GetUserMealsById : IGetUserMealsById{

    private readonly Context _db;

    public GetUserMealsById(Context db)
    {
        _db = db;
    }
    public async Task<UserMealsResponse> ExecuteAsync(Guid userId, UserMealsIdRequest request)
    {
        var user = await _db.Users.AsNoTracking().Where(a => a.Id == userId).AnyAsync();

        if(!user) throw new InvalidOperationException("Usuário não encontrado.");

        var result = await _db.UserMeal
        .AsNoTracking()
        .Where(a => a.UserId == userId && a.Id == request.MealId)
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
        )).FirstOrDefaultAsync();

        if(result == null) throw new InvalidOperationException("Refeição não encontrada.");

        return result;
    }
}
