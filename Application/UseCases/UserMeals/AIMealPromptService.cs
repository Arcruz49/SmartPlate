using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.Interfaces;
using SmartPlate.Application.DTOs.Prompts;
using System.Text.Json;
using SmartPlate.Application.DTOs.Request;

namespace SmartPlate.Application.UseCases;

public class AIMealPromptService : IAMealPromptService{    
    public AIMealPromptService()
    {
    }
    public async Task<string> ExecuteAsync(UserMealInput userMeal)
    {
        var prompt = new UserMealPrompt
        {
            system_instruction = "You are an expert AI Sports Nutritionist. Calculate precise nutritional and recovery targets. " +
                             "Methodology: Use Mifflin-St Jeor for BMR. Apply Activity Factor based on training and daily levels. " +
                             "Goal Adjustment: Apply caloric deficit for 'lose_fat' or surplus for 'gain_muscle'.",

            user_meal = userMeal,

            response_format = new ResponseFormatUserMeal
            {
                type = "json_object",
                schema = new InsightSchemaUserMeal
                {
                    calories = "number (total)",
                    protein_g = "number (total grams)",
                    carbs_g = "number (total grams)",
                    fat_g = "number (total grams)"
                }
            },

            instruction = "Return ONLY the JSON object that strictly follows the keys defined in the schema above."
        };

        return JsonSerializer.Serialize(prompt);

    }
}
