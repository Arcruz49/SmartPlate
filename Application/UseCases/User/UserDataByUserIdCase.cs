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
        var userData = await _db.user_data.AsNoTracking().FirstOrDefaultAsync(a => a.user_id == userId);

        if(userData == null) throw new InvalidOperationException("Usuário ainda não possui dados cadastrados.");

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
