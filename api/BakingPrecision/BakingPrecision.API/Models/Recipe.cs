namespace BakingPrecision.API.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Instructions { get; set; }
        public string? SourceUrl { get; set; }
        public int? PrepTimeMinutes { get; set; }
        public int? CookTimeMinutes { get; set; }
        public string? Yield { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public List<RecipeIngredient> Ingredients { get; set; } = new();
        public List<RecipeStep> Steps { get; set; } = new();
        public Media? Media { get; set; }




    }
}
