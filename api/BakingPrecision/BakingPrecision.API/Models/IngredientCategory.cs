namespace BakingPrecision.API.Models
{
    public class IngredientCategory
    {
        public int Id { get; set; }
        //The display name of the category (e.g., "Flours", "Sugars", "Fats") will be used for the UI
        public string Name { get; set; } = string.Empty;
        //check all ingredients that belong to that category
        //public List<Ingredient> Ingredients { get; set; } = new();
    }
}
