using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.Interfaces;
using SmartPlate.Infrastructure.Data;
using SmartPlate.Application.DTOs.Prompts;
using System.Text.Json;

namespace SmartPlate.Application.UseCases;

public class AIInsightsPromptService : IAIInsightsPromptService
{
    public AIInsightsPromptService()
    {
    }

    public async Task<string> ExecuteAsync(UserDataResponse userData)
    {
        var prompt = new UserDataInsightPrompt
        {
            system_instruction =
                "You are an expert AI Sports Nutritionist focused on evidence-based performance nutrition. " +
                "Your task is to calculate realistic daily nutritional targets and recovery needs using accepted sports science principles. " +
                "Methodology rules: " +
                "1. Calculate BMR using the Mifflin-St Jeor equation. " +
                "2. Apply an activity multiplier based on workouts, training intensity, and daily activity. " +
                "3. Adjust calories according to the user's goal (moderate deficit for fat loss, moderate surplus for muscle gain, maintenance otherwise). " +
                "4. Set protein targets based on body weight and training level. " +
                "5. Distribute remaining calories into carbs and fats using balanced athletic ratios. " +
                "6. Recommend sleep duration based on recovery needs and stress level. " +
                "Prefer realistic, sustainable values suitable for long-term adherence. " +
                "Avoid extreme cutting or bulking recommendations.",

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

            response_format = new ResponseFormatUserData
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

            instruction =
                "Return ONLY a valid JSON object following the schema keys. " +
                "Rules: numbers only, no units, no text explanations, no extra fields, " +
                "no markdown, no comments, only clean JSON. " +
                "Values must be realistic and internally consistent with caloric totals."
        };

        return JsonSerializer.Serialize(prompt);
    }
}
