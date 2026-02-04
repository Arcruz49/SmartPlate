using Microsoft.EntityFrameworkCore;
using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.Interfaces;
using SmartPlate.Domain.Enums;
using SmartPlate.Infrastructure.Data;


namespace SmartPlate.Application.UseCases;

public class UserMealMetricsCase : IUserMealMetricsCase{

    private readonly Context _db;

    public UserMealMetricsCase(Context db)
    {
        _db = db;
    }
    public async Task<List<UserMealsMetricsResponse>> ExecuteAsync(Guid userId, UserMealsMetricsRequest request)
    {
        var today = DateTime.UtcNow.Date;

        DateTime startDate = request.MetricsTime switch
        {
            MetricsTime.week => today.AddDays(-7),
            MetricsTime.month => today.AddMonths(-1),
            _ => today
        };

        return await _db.UserMeal
            .AsNoTracking()
            .Where(m => m.UserId == userId && m.MealDate >= startDate)
            .GroupBy(m => m.MealDate)
            .Select(g => new UserMealsMetricsResponse(
                g.Key,
                g.Sum(x => x.Calories),
                g.Sum(x => x.ProteinG),
                g.Sum(x => x.CarbsG),
                g.Sum(x => x.FatG)
            ))
            .ToListAsync();
    }

}
