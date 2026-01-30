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
        var user = await _db.users.AsNoTracking().Where(a => a.id == userId).AnyAsync();

        if(!user) throw new InvalidOperationException("Usuário não encontrado.");

        var userData = new UserData
        {
            id = Guid.NewGuid(),
            user_id = userId,
            weight_kg = request.WeightKg,
            height_cm = request.HeightCm,
            age = request.Age,
            biological_sex = request.BiologicalSex,
            workouts_per_week = request.WorkoutsPerWeek,
            training_intensity = request.TrainingIntensity,
            training_type = request.TrainingType,
            daily_activity_level = request.DailyActivityLevel,
            user_goal = request.Goal,
            sleep_quality = request.SleepQuality,
            stress_level = request.StressLevel,
            routine_consistency = request.RoutineConsistency
        };

        _db.user_data.Add(userData);
        await _db.SaveChangesAsync();

        return new UserDataResponse(
            userData.weight_kg,
            userData.height_cm,
            userData.age,
            userData.biological_sex,
            userData.workouts_per_week,
            userData.training_type,
            userData.training_intensity,
            userData.daily_activity_level,
            userData.user_goal,
            userData.sleep_quality,
            userData.stress_level,
            userData.routine_consistency
        );
    }
}
