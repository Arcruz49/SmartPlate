using Microsoft.EntityFrameworkCore;
using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.Interfaces;
using SmartPlate.Infrastructure.Data;

namespace SmartPlate.Application.UseCases;

public class UserDataCreateCase : IUserDataCreateCase
{
    private readonly Context _db;

    public UserDataCreateCase(Context db)
    {
        _db = db;
    }
    public async Task<UserDataResponse> ExecuteAsync(Guid userId, UserDataRequest request)
    {
        var userExists = await _db.Users
            .AsNoTracking()
            .AnyAsync(u => u.Id == userId);

        if (!userExists)
            throw new InvalidOperationException("Usuário não encontrado.");

        var userData = await _db.UserData
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (userData == null)
        {
            userData = new Domain.Entities.UserData
            {
                Id = Guid.NewGuid(),
                UserId = userId
            };

            _db.UserData.Add(userData);
        }

        userData.WeightKg = request.WeightKg;
        userData.HeightCm = request.HeightCm;
        userData.Age = request.Age;
        userData.BiologicalSex = request.BiologicalSex;
        userData.WorkoutsPerWeek = request.WorkoutsPerWeek;
        userData.TrainingIntensity = request.TrainingIntensity;
        userData.TrainingType = request.TrainingType;
        userData.DailyActivityLevel = request.DailyActivityLevel;
        userData.UserGoal = request.Goal;
        userData.SleepQuality = request.SleepQuality;
        userData.StressLevel = request.StressLevel;
        userData.RoutineConsistency = request.RoutineConsistency;
        userData.WorkoutDetails = request.WorkoutDetails;
        userData.DailyActivityDetails = request.DailyActivityDetails;
        userData.UpdatedAt = DateTime.UtcNow;

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
            userData.RoutineConsistency,
            userData.WorkoutDetails,
            userData.DailyActivityDetails
        );
    }
}
