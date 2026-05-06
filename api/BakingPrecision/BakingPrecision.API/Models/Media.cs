namespace BakingPrecision.API.Models
{
    public class Media
    {
        public int Id { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null;

    }
}
