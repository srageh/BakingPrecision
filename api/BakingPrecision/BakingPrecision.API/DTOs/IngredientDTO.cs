namespace BakingPrecision.API.DTOs
{
    public class IngredientDTO
    {
        public string Name { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal GramWeight { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
