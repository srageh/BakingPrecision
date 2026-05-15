using BakingPrecision.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace BakingPrecision.API.Controllers
{
    [Route("api/recipe-extraction")]
    [ApiController]
    public class RecipeExtractionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public RecipeExtractionController(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        [HttpPost("from-image")]
        public async Task<ActionResult<RecipeDTO>> ExtractFromImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("No image was provided.");
            }

            // 1. Convert Image to Base64
            using var memoryStream = new MemoryStream();
            await image.CopyToAsync(memoryStream);
            var base64Image = Convert.ToBase64String(memoryStream.ToArray());
            var mimeType = image.ContentType;

            // 2. Define the System Instructions
            var systemInstructions = @"
BakingPrecision AI Extraction System Instructions:
1. FRACTION TO DECIMAL: Convert all fractional strings to decimal numbers (e.g., '1/2' -> 0.5, '1 3/4' -> 1.75). Never output a slash.
2. WEIGHT FALLBACK: Provide a gramWeight based on standard culinary density to act as a fallback. If unit is 'g' or 'kg', gramWeight is the same as quantity.
3. CATEGORY MAPPING: Assign to one of: 'Flours', 'Sugars', 'Fats', 'Dairy', 'Leaveners', 'Liquids', 'Spices', 'Other'.
4. MISSING UNITS: If an ingredient is discrete and has no formal volume/weight unit (e.g., '3 Large Eggs'), set the unit to an empty string """". Do not use null.
5. EDGE CASES: Missing steps = []. 'to taste' = 0 quantity and 'pinch' unit. If a property like Title or Prep Time is not explicitly written on the card, omit it entirely. Do not guess.";

            // Build the Payload with Strict JSON Schema
            var payload = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new object[]
                        {
                            new { text = systemInstructions },
                            new { inline_data = new { mime_type = mimeType, data = base64Image } }
                        }
                    }
                },
                generationConfig = new
                {
                    responseMimeType = "application/json",
                    responseSchema = new
                    {
                        type = "OBJECT",
                        properties = new Dictionary<string, object>
                        {
                            { "title", new { type = "STRING" } },
                            { "sourceUrl", new { type = "STRING" } },
                            { "prepTimeMinutes", new { type = "INTEGER" } },
                            { "cookTimeMinutes", new { type = "INTEGER" } },
                            { "yield", new { type = "STRING" } },
                            { "ingredients", new
                                {
                                    type = "ARRAY",
                                    items = new
                                    {
                                        type = "OBJECT",
                                        properties = new Dictionary<string, object>
                                        {
                                            { "name", new { type = "STRING" } },
                                            { "quantity", new { type = "NUMBER" } },
                                            { "unit", new { type = "STRING" } },
                                            { "gramWeight", new { type = "NUMBER" } },
                                            { "categoryName", new { type = "STRING" } }
                                        },
                                        required = new[] { "name", "quantity", "unit", "gramWeight", "categoryName" }
                                    }
                                }
                            },
                            { "instructions", new
                                {
                                    type = "ARRAY",
                                    items = new
                                    {
                                        type = "OBJECT",
                                        properties = new Dictionary<string, object>
                                        {
                                            { "stepNumber", new { type = "INTEGER" } },
                                            { "instruction", new { type = "STRING" } }
                                        },
                                        required = new[] { "stepNumber", "instruction" }
                                    }
                                }
                            }
                        },
                        required = new[] { "ingredients" }
                    }
                }
            };

            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // Send Request to Gemini API
            var apiKey = _configuration["GeminiApiKey"];
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return StatusCode(500, $"AI API Error: {error}");
            }

            // Parse the AI Response
            var responseString = await response.Content.ReadAsStringAsync();
            using var jsonDocument = JsonDocument.Parse(responseString);

            var extractedJsonString = jsonDocument.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            // Deserialize directly into your DTO
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var recipeDto = JsonSerializer.Deserialize<RecipeDTO>(extractedJsonString!, options);

            return Ok(recipeDto);
        }
    }
}