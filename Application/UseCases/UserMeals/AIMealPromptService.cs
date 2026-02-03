using SmartPlate.Application.DTOs.Responses;
using SmartPlate.Application.Interfaces;
using SmartPlate.Application.DTOs.Prompts;
using System.Text.Json;
using SmartPlate.Application.DTOs.Request;

namespace SmartPlate.Application.UseCases;

public class AIMealPromptService : IAMealPromptService
{
    public AIMealPromptService()
    {
    }

    public async Task<string> ExecuteAsync(UserMealsRequest userMeal)
    {
        var prompt = new UserMealPrompt
        {
            system_instruction =
                "You are a professional Dietitian specialized in visual food recognition and portion estimation. " +
                "Carefully analyze the meal image as if it were a restaurant plate. " +
                "You must identify all visible food items, estimate realistic portion sizes based on visual volume, " +
                "and calculate total calories and macronutrients using common nutritional averages. " +
                "Internally reason per item before calculating totals. " +
                "Prefer conservative and realistic estimates instead of extreme values. " +
                "If uncertain, choose the most probable estimate based on common meals. " +
                "Your goal is consistency and realism, not perfection.",

            user_meal = new UserMealInput
            {
                meal_Name = userMeal.MealName,
                description = userMeal.Description,
            },

            response_format = new ResponseFormatUserMeal
            {
                type = "json_object",
                schema = new InsightSchemaUserMeal
                {
                    calories = "number (total kcal)",
                    protein_g = "number (total grams)",
                    carbs_g = "number (total grams)",
                    fat_g = "number (total grams)",
                    explanation = "short portuguese explanation of how the nutritional estimates were derived (max 800 characters)",
                    advice = "short portuguese nutritional advice or suggestion based on meal (max 200 characters)"

                }
            },

            instruction =
                "Use the image as the primary source and the description as additional context. " +
                "Return ONLY the JSON object following the schema keys. " +
                "Rules: numeric fields must contain numbers only with no units or text. " +
                "The 'explanation' field must contain a short Portuguese explanation (max 800 characters) about how you estimated the nutritional values. " +
                "The 'advice' field must contain a brief Portuguese nutritional suggestion or tip based on the meal (max 200 characters), e.g., reduce sugar, add protein, etc. " +
                "No extra fields, no commentary outside JSON, no markdown, only valid JSON."

        };

        return JsonSerializer.Serialize(prompt);
    }
}
