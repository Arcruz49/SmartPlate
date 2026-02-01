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
                    fat_g = "number (total grams)"
                }
            },

            instruction =
                "Use the image as the primary source and the description as additional context. " +
                "Return ONLY the JSON object following the schema keys. " +
                "Rules: numbers only, no text or units, no explanations, no extra fields, " +
                "no commentary, no markdown, only valid JSON. " +
                "If no image is available, rely solely on the description and standard portion assumptions."
        };

        return JsonSerializer.Serialize(prompt);
    }
}
