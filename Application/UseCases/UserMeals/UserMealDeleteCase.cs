using Microsoft.EntityFrameworkCore;
using SmartPlate.Application.DTOs;
using SmartPlate.Application.Interfaces;
using SmartPlate.Infrastructure.Data;


namespace SmartPlate.Application.UseCases;

public class UserMealDeleteCase : IUserMealsDelete{

    private readonly Context _db;

    public UserMealDeleteCase(Context db)
    {
        _db = db;
    }
    public async Task ExecuteAsync(Guid mealId, Guid userId)
    {
        var user = await _db.Users.AsNoTracking().Where(a => a.Id == userId).AnyAsync();

        if(!user) throw new InvalidOperationException("Usuário não encontrado.");

        var userMeal = await _db.UserMeal.AsNoTracking().Where(a => a.UserId == userId && a.Id == mealId).FirstOrDefaultAsync();

        if (userMeal == null) throw new InvalidOperationException("Refeição não encontrada");

        _db.UserMeal.Remove(userMeal);

        await _db.SaveChangesAsync();
    }
}
