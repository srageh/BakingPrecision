namespace BakingPrecision.API.DTOs
{
    public class RecipeListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int? PrepTimeMinutes { get; set; }
        public int? CookTimeMinutes { get; set; }
    }
}
