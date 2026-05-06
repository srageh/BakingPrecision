namespace BakingPrecision.API.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        // Foreign Key to the Category (e.g., "Flour" belongs to the "Dry Goods" category)
        public int IngredientCategoryId { get; set; }

        // Navigation property to the Category object
        public IngredientCategory Category { get; set; } = null!;
        //allows to retrive all the conversions easily 
        public List<IngredientConversion> Conversions { get; set; } = new();
    }
}
