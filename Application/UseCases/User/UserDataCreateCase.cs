using Microsoft.EntityFrameworkCore;
using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.Interfaces;
using SmartPlate.Infrastructure.Data;
using SmartPlate.Domain.Entities;

namespace SmartPlate.Application.UseCases;

public class UserDataCreateCase : IUserDataCreateCase{

    private readonly Context _db;
    
    public UserDataCreateCase(Context db)
    {
        _db = db;
    }
    public async Task<UserDataResponse> ExecuteAsync(Guid userId, UserDataRequest request)
    {
        var user = await _db.Users.AsNoTracking().Where(a => a.Id == userId).AnyAsync();

        if(!user) throw new InvalidOperationException("Usuário não encontrado.");

        var userData = new UserData
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            WeightKg = request.WeightKg,
            HeightCm = request.HeightCm,
            Age = request.Age,
            BiologicalSex = request.BiologicalSex,
            WorkoutsPerWeek = request.WorkoutsPerWeek,
            TrainingIntensity = request.TrainingIntensity,
            TrainingType = request.TrainingType,
            DailyActivityLevel = request.DailyActivityLevel,
            UserGoal = request.Goal,
            SleepQuality = request.SleepQuality,
            StressLevel = request.StressLevel,
            RoutineConsistency = request.RoutineConsistency
        };

        _db.UserData.Add(userData);
        await _db.SaveChangesAsync();

        return new UserDataResponse(
            userData.WeightKg,
            userData.HeightCm,
            userData.Age,
            userData.BiologicalSex,
            userData.WorkoutsPerWeek,
            userData.TrainingType,
            userData.TrainingIntensity,
            userData.DailyActivityLevel,
            userData.UserGoal,
            userData.SleepQuality,
            userData.StressLevel,
            userData.RoutineConsistency
        );
    }
}
