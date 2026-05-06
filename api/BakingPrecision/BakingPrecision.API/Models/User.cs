namespace BakingPrecision.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Recipe> Recipes { get; set; } = new();

    }
}
