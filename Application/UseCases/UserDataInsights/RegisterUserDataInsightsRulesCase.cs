using Microsoft.EntityFrameworkCore;
using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.Interfaces;
using SmartPlate.Domain.Entities;
using SmartPlate.Infrastructure.Data;


namespace SmartPlate.Application.UseCases;

public class RegisterUserDataInsightsRulesCase : IRegisterUserDataInsightsRulesCase{

    private readonly Context _db;
    
    public RegisterUserDataInsightsRulesCase(Context db)
    {
        _db = db;
        
    }
    public async Task<UserDataInsightsResponse> ExecuteAsync(Guid userId, UserDataInsightRequest userDataInsightRequest)
    {
        var user = await _db.Users.AsNoTracking().Where(a => a.Id == userId).AnyAsync();

        if(!user) throw new InvalidOperationException("Usuário não encontrado.");

        var userDataInsight = await _db.UserDataInsights.FirstOrDefaultAsync(a => a.UserId == userId);

        if(userDataInsightRequest.TargetCalories <= 0) throw new InvalidOperationException("Valor de calorias inválido.");
        if(userDataInsightRequest.ProteingTargetG <= 0) throw new InvalidOperationException("Valor de proteínas inválido.");
        if(userDataInsightRequest.CarbsTargetG <= 0) throw new InvalidOperationException("Valor de carboidratos inválido.");
        if(userDataInsightRequest.FatTargetG <= 0)  throw new InvalidOperationException("Valor de gorduras inválido.");

        if(userDataInsight == null)
        {
            userDataInsight = new UserDataInsights()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                TargetCalories = userDataInsightRequest.TargetCalories,
                ProteinTargetG = userDataInsightRequest.ProteingTargetG,
                CarbsTargetG = userDataInsightRequest.CarbsTargetG,
                FatTargetG = userDataInsightRequest.FatTargetG,
                CreatedAt = DateTime.Now
            };

            _db.UserDataInsights.Add(userDataInsight);
        }
        else
        {
            userDataInsight.TargetCalories = userDataInsightRequest.TargetCalories;
            userDataInsight.ProteinTargetG = userDataInsightRequest.ProteingTargetG;
            userDataInsight.CarbsTargetG = userDataInsightRequest.CarbsTargetG;
            userDataInsight.FatTargetG = userDataInsightRequest.FatTargetG;
        }

        userDataInsight.AiGenerated = false;

        await _db.SaveChangesAsync();
        
        return new UserDataInsightsResponse(
            userDataInsight.TargetCalories ?? 0,
            userDataInsight.ProteinTargetG ?? 0, 
            userDataInsight.CarbsTargetG ?? 0, 
            userDataInsight.FatTargetG ?? 0, 
            userDataInsight.SleepHoursTarget ?? 0
        );

    }
}
