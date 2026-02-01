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
    public async Task<string> ExecuteAsync(UserMealsRequest userMeal)
    {
        var prompt = new UserMealPrompt
        {
            system_instruction = "You are a professional Dietitian specialized in food recognition. " +
                                "Your task is to analyze the provided image and description of a meal. " +
                                "1. Identify all food items and estimate their weight/portion size. " +
                                "2. Calculate total calories and macronutrients (protein, carbs, fats). " +
                                "3. Be realistic and precise based on visual volume.",

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

            instruction = "Analyze both the image and description. Return ONLY the JSON object following the schema keys. If no image is provided, rely solely on the description."
        };

        return JsonSerializer.Serialize(prompt);
    }
}
