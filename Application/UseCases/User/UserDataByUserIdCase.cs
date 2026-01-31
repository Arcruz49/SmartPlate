using Microsoft.EntityFrameworkCore;
using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.Interfaces;
using SmartPlate.Infrastructure.Data;


namespace SmartPlate.Application.UseCases;

public class UserDataByUserIdCase : IUserDataByUserIdCase{
    private readonly Context _db;
    public UserDataByUserIdCase(Context db)
    {
        _db = db;
    }
    public async Task<UserDataResponse> ExecuteAsync(Guid userId)
    {
        var userData = await _db.user_data.AsNoTracking().FirstOrDefaultAsync(a => a.UserId == userId);

        if(userData == null) throw new InvalidOperationException("Usuário ainda não possui dados cadastrados.");

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
