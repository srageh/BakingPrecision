using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BakingPrecision.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePrecisionModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instructions",
                table: "Recipes");

            migrationBuilder.AddColumn<decimal>(
                name: "GramWeight",
                table: "RecipeIngredients",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GramWeight",
                table: "RecipeIngredients");

            migrationBuilder.AddColumn<string>(
                name: "Instructions",
                table: "Recipes",
                type: "TEXT",
                nullable: true);
        }
    }
}
