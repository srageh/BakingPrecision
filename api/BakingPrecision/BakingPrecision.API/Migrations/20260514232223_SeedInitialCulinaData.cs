using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BakingPrecision.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialCulinaData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "IngredientConversions",
                columns: new[] { "Id", "GramsPerUnit", "IngredientId", "UnitId" },
                values: new object[,]
                {
                    { 1, 120.0m, 1, 4 },
                    { 2, 200.0m, 2, 4 },
                    { 3, 227.0m, 3, 4 },
                    { 4, 236.0m, 5, 4 },
                    { 5, 240.0m, 6, 4 },
                    { 6, 6.0m, 7, 5 },
                    { 7, 6.0m, 8, 5 },
                    { 8, 4.0m, 9, 5 }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "IngredientCategoryId", "Name" },
                values: new object[] { 10, 2, "Brown Sugar" });

            migrationBuilder.InsertData(
                table: "IngredientConversions",
                columns: new[] { "Id", "GramsPerUnit", "IngredientId", "UnitId" },
                values: new object[] { 9, 213.0m, 10, 4 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IngredientConversions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IngredientConversions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "IngredientConversions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "IngredientConversions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "IngredientConversions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "IngredientConversions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "IngredientConversions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "IngredientConversions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "IngredientConversions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
