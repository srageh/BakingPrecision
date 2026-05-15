namespace BakingPrecision.API.DTOs
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? SourceUrl { get; set; }
        public int? PrepTimeMinutes { get; set; }
        public int? CookTimeMinutes { get; set; }
        public string? Yield { get; set; }
        public List<IngredientDTO> Ingredients { get; set; } = new();
        public List<InstructionDto> Instructions { get; set; } = new();


    }

    

   
}
