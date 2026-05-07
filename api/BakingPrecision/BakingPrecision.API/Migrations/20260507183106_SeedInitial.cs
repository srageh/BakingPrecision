using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BakingPrecision.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "IngredientCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Flours" },
                    { 2, "Sugars" },
                    { 3, "Fats" },
                    { 4, "Dairy" },
                    { 5, "Leaveners" },
                    { 6, "Liquids" },
                    { 7, "Spices" },
                    { 8, "Other" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Abbreviation", "Name", "System" },
                values: new object[,]
                {
                    { 1, "g", "Gram", "Metric" },
                    { 2, "kg", "Kilogram", "Metric" },
                    { 3, "ml", "Milliliter", "Metric" },
                    { 4, "cup", "Cup", "US Customary" },
                    { 5, "tsp", "Teaspoon", "US Customary" },
                    { 6, "tbsp", "Tablespoon", "US Customary" },
                    { 7, "unit", "Unit", "Metric" },
                    { 8, "oz", "Ounce", "US Customary" },
                    { 9, "lb", "Pound", "US Customary" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "IngredientCategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "All-Purpose Flour" },
                    { 2, 2, "Granulated Sugar" },
                    { 3, 3, "Unsalted Butter" },
                    { 4, 4, "Large Egg" },
                    { 5, 6, "Water" },
                    { 6, 4, "Whole Milk" },
                    { 7, 7, "Table Salt" },
                    { 8, 5, "Baking Soda" },
                    { 9, 5, "Baking Powder" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IngredientCategories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "IngredientCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IngredientCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "IngredientCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "IngredientCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "IngredientCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "IngredientCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "IngredientCategories",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
