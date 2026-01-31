using Microsoft.EntityFrameworkCore;
using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.Interfaces;
using SmartPlate.Domain.Entities;
using SmartPlate.Infrastructure.AI;
using SmartPlate.Infrastructure.Data;


namespace SmartPlate.Application.UseCases;

public class UserDataInsightsCreateCase : IUserDataInsightsCreateCase{

    private readonly Context _db;
    private readonly IAIInsightsPromptService _aiInsightsPromptService;
    private readonly IUserDataByUserIdCase _userDataByUserIdCase;
    private readonly IAIClient _aiClient;
    private readonly IParseGeminiUserInsightsCase _parseGeminiUserInsightsCase;
    
    public UserDataInsightsCreateCase(Context db, IAIInsightsPromptService aiInsightsPromptService, IUserDataByUserIdCase userDataByUserIdCase, IAIClient aiClient,
     IParseGeminiUserInsightsCase parseGeminiUserInsightsCase)
    {
        _db = db;
        _aiInsightsPromptService = aiInsightsPromptService;
        _userDataByUserIdCase = userDataByUserIdCase;
        _aiClient = aiClient;
        _parseGeminiUserInsightsCase = parseGeminiUserInsightsCase;
    }
    public async Task<UserDataInsightsResponse> ExecuteAsync(Guid userId)
    {
        var user = await _db.Users.AsNoTracking().Where(a => a.Id == userId).AnyAsync();

        if(!user) throw new InvalidOperationException("Usuário não encontrado.");

        var userData = await _userDataByUserIdCase.ExecuteAsync(userId) ?? throw new InvalidOperationException("Dados do usuário não encontrados.");

        var prompt = await _aiInsightsPromptService.ExecuteAsync(userData);

        var result = await _aiClient.SendPromptAsync(prompt);

        var nutritionInsight = await _parseGeminiUserInsightsCase.ExecuteAsync(result);

        var userDataInsight = new UserDataInsights
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            TargetCalories = nutritionInsight.target_calories,
            CarbsTargetG = nutritionInsight.carbs_target_g,
            ProteinTargetG = nutritionInsight.protein_target_g,
            FatTargetG = nutritionInsight.fat_target_g,
            SleepHoursTarget = nutritionInsight.sleep_hours_target
        };

        _db.UserDataInsights.Add(userDataInsight);
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
