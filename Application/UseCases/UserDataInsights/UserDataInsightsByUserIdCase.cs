using Microsoft.EntityFrameworkCore;
using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.Interfaces;
using SmartPlate.Domain.Entities;
using SmartPlate.Infrastructure.AI;
using SmartPlate.Infrastructure.Data;


namespace SmartPlate.Application.UseCases;

public class UserDataInsightsByUserIdCase : IUserDataInsightsByUserIdCase{

    private readonly Context _db;
    
    public UserDataInsightsByUserIdCase(Context db)
    {
        _db = db;
        
    }
    public async Task<UserDataInsightsResponse> ExecuteAsync(Guid userId)
    {
        var user = await _db.Users.AsNoTracking().Where(a => a.Id == userId).AnyAsync();

        if(!user) throw new InvalidOperationException("Usuário não encontrado.");

        var userDataInsight = await _db.UserDataInsights.AsNoTracking().FirstOrDefaultAsync(a => a.UserId == userId) ?? throw new InvalidOperationException("Dados não encontrados");

        return new UserDataInsightsResponse(
            userDataInsight.TargetCalories ?? 0,
            userDataInsight.ProteinTargetG ?? 0, 
            userDataInsight.CarbsTargetG ?? 0, 
            userDataInsight.FatTargetG ?? 0, 
            userDataInsight.SleepHoursTarget ?? 0
        );

    }
}
