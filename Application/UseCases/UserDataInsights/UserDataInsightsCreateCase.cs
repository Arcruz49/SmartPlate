using Microsoft.EntityFrameworkCore;
using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.Interfaces;
using SmartPlate.Domain.Entities;
using SmartPlate.Infrastructure.AI;
using SmartPlate.Infrastructure.Data;

namespace SmartPlate.Application.UseCases;

public class UserDataInsightsCreateCase : IUserDataInsightsCreateCase
{
    private readonly Context _db;
    private readonly IAIInsightsPromptService _aiInsightsPromptService;
    private readonly IUserDataByUserIdCase _userDataByUserIdCase;
    private readonly IAIClient _aiClient;
    private readonly IParseGeminiUserInsightsCase _parseGeminiUserInsightsCase;

    public UserDataInsightsCreateCase(
        Context db,
        IAIInsightsPromptService aiInsightsPromptService,
        IUserDataByUserIdCase userDataByUserIdCase,
        IAIClient aiClient,
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
        var userExists = await _db.Users
            .AsNoTracking()
            .AnyAsync(u => u.Id == userId);

        if (!userExists)
            throw new InvalidOperationException("Usuário não encontrado.");

        var userData = await _userDataByUserIdCase.ExecuteAsync(userId)
            ?? throw new InvalidOperationException("Dados do usuário não encontrados.");

        var prompt = await _aiInsightsPromptService.ExecuteAsync(userData);

        var aiRawResponse = await _aiClient.SendPromptAsync(prompt);

        var nutritionInsight = await _parseGeminiUserInsightsCase.ExecuteAsync(aiRawResponse);

        var existingInsight = await _db.UserDataInsights
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (existingInsight == null)
        {
            existingInsight = new UserDataInsights
            {
                Id = Guid.NewGuid(),
                UserId = userId
            };

            _db.UserDataInsights.Add(existingInsight);
        }

        existingInsight.TargetCalories   = nutritionInsight.target_calories;
        existingInsight.ProteinTargetG   = nutritionInsight.protein_target_g;
        existingInsight.CarbsTargetG     = nutritionInsight.carbs_target_g;
        existingInsight.FatTargetG       = nutritionInsight.fat_target_g;
        existingInsight.SleepHoursTarget = nutritionInsight.sleep_hours_target;

        await _db.SaveChangesAsync();

        return new UserDataInsightsResponse(
            existingInsight.TargetCalories ?? 0,
            existingInsight.ProteinTargetG ?? 0,
            existingInsight.CarbsTargetG ?? 0,
            existingInsight.FatTargetG ?? 0,
            existingInsight.SleepHoursTarget ?? 0
        );
    }
}
