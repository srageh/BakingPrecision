namespace BakingPrecision.API.DTOs
{
    public class RecipeDto
    {
        public string Title { get; set; } = string.Empty;
        public string? SourceUrl { get; set; }
        public int? PrepTimeMinutes { get; set; }
        public int? CookTimeMinutes { get; set; }
        public string? Yield { get; set; }
        public List<IngredientDto> Ingredients { get; set; } = new();
        public List<InstructionDto> Instructions { get; set; } = new();


    }

    public class IngredientDTO
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal GramWeight { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

   
}
