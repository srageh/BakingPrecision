namespace BakingPrecision.API.Models
{
    public class IngredientConversion
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null;
        public int UnitId { get; set; }
        public Unit Unit { get; set; } = null;

        //ex: converts 1 cup of flour to 120.0 g
        public decimal GramsPerUnit { get; set; }
    }
}
