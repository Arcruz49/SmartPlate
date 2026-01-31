using Microsoft.EntityFrameworkCore;
using SmartPlate.Application.DTOs.Request;
using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.Interfaces;
using SmartPlate.Infrastructure.Data;
using SmartPlate.Domain.Entities;
using SmartPlate.Application.DTOs.Prompts;

namespace SmartPlate.Application.UseCases;

public class UserDataInsightsCreateCase : IAIInsightsPromptService{

    private readonly Context _db;
    
    public UserDataInsightsCreateCase(Context db, IUserDataByUserIdCase userDataByUserIdCase)
    {
        _db = db;
    }
    public async Task<UserDataInsightPrompt> ExecuteAsync(UserDataRequest userData)
    {
        var prompt = new UserDataInsightPrompt
        {
            system_instruction = "You are an expert AI Sports Nutritionist. Calculate precise nutritional and recovery targets. " +
                             "Methodology: Use Mifflin-St Jeor for BMR. Apply Activity Factor based on training and daily levels. " +
                             "Goal Adjustment: Apply caloric deficit for 'lose_fat' or surplus for 'gain_muscle'.",

            user_data = new UserDataInput
            {
                weight_kg = userData.WeightKg,
                height_cm = userData.HeightCm,
                age = userData.Age,
                biological_sex = userData.BiologicalSex.ToString(),
                workouts_per_week = userData.WorkoutsPerWeek,
                training_type = userData.TrainingType.ToString(),
                training_intensity = userData.TrainingIntensity.ToString(),
                daily_activity_level = userData.DailyActivityLevel.ToString(),
                user_goal = userData.Goal.ToString(),
                sleep_quality = userData.SleepQuality,
                stress_level = userData.StressLevel,
                routine_consistency = userData.RoutineConsistency
            
            },

            response_format = new ResponseFormat
            {
                type = "json_object",
                schema = new InsightSchemaUserData
                {
                    target_calories = "number (daily total)",
                    protein_target_g = "number (total grams)",
                    carbs_target_g = "number (total grams)",
                    fat_target_g = "number (total grams)",
                    sleep_hours_target = "number (recommended hours)"
                }
            },

            instruction = "Return ONLY the JSON object that strictly follows the keys defined in the schema above."
        };

        return prompt;

    }
}
