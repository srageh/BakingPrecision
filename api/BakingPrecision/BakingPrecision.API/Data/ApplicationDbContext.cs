using BakingPrecision.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BakingPrecision.API.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Recipe> Recipes { get; set; } = null!;
        public DbSet<Ingredient> Ingredients { get; set; } = null!;
        public DbSet<IngredientCategory> IngredientCategories { get; set; } = null!;
        public DbSet<Unit> Units { get; set; } = null!;
        public DbSet<IngredientConversion> IngredientConversions { get; set; } = null!;
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; } = null!;
        public DbSet<RecipeStep> RecipeSteps { get; set; } = null!;
        public DbSet<Media> MediaFiles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Unit>().HasData(
                new Unit { Id = 1, Name = "Gram", Abbreviation = "g", System = "Metric" },
                new Unit { Id = 2, Name = "Kilogram", Abbreviation = "kg", System = "Metric" },
                new Unit { Id = 3, Name = "Milliliter", Abbreviation = "ml", System = "Metric" },
                new Unit { Id = 4, Name = "Cup", Abbreviation = "cup", System = "US Customary" },
                new Unit { Id = 5, Name = "Teaspoon", Abbreviation = "tsp", System = "US Customary" },
                new Unit { Id = 6, Name = "Tablespoon", Abbreviation = "tbsp", System = "US Customary" },
                new Unit { Id = 7, Name = "Unit", Abbreviation = "unit", System = "Metric" },
                new Unit { Id = 8, Name = "Ounce", Abbreviation = "oz", System = "US Customary" },
                new Unit { Id = 9, Name = "Pound", Abbreviation = "lb", System = "US Customary" }
            );

            modelBuilder.Entity<IngredientCategory>().HasData(
                new IngredientCategory { Id = 1, Name = "Flours" },
                new IngredientCategory { Id = 2, Name = "Sugars" },
                new IngredientCategory { Id = 3, Name = "Fats" },
                new IngredientCategory { Id = 4, Name = "Dairy" },
                new IngredientCategory { Id = 5, Name = "Leaveners" },
                new IngredientCategory { Id = 6, Name = "Liquids" },
                new IngredientCategory { Id = 7, Name = "Spices" },
                new IngredientCategory { Id = 8, Name = "Other" }
            );

            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, Name = "All-Purpose Flour", IngredientCategoryId = 1 },
                new Ingredient { Id = 2, Name = "Granulated Sugar", IngredientCategoryId = 2 },
                new Ingredient { Id = 3, Name = "Unsalted Butter", IngredientCategoryId = 3 },
                new Ingredient { Id = 4, Name = "Large Egg", IngredientCategoryId = 4 },
                new Ingredient { Id = 5, Name = "Water", IngredientCategoryId = 6 },
                new Ingredient { Id = 6, Name = "Whole Milk", IngredientCategoryId = 4 },
                new Ingredient { Id = 7, Name = "Table Salt", IngredientCategoryId = 7 },
                new Ingredient { Id = 8, Name = "Baking Soda", IngredientCategoryId = 5 },
                new Ingredient { Id = 9, Name = "Baking Powder", IngredientCategoryId = 5 },
                new Ingredient { Id = 10, Name = "Brown Sugar", IngredientCategoryId = 2 }
            );

            // Seed data for precise mathematical conversions
            modelBuilder.Entity<IngredientConversion>().HasData(
                new IngredientConversion { Id = 1, IngredientId = 1, UnitId = 4, GramsPerUnit = 120.0m },

                new IngredientConversion { Id = 2, IngredientId = 2, UnitId = 4, GramsPerUnit = 200.0m },

                new IngredientConversion { Id = 3, IngredientId = 3, UnitId = 4, GramsPerUnit = 227.0m },

                new IngredientConversion { Id = 4, IngredientId = 5, UnitId = 4, GramsPerUnit = 236.0m },

                new IngredientConversion { Id = 5, IngredientId = 6, UnitId = 4, GramsPerUnit = 240.0m },

                new IngredientConversion { Id = 6, IngredientId = 7, UnitId = 5, GramsPerUnit = 6.0m },

                new IngredientConversion { Id = 7, IngredientId = 8, UnitId = 5, GramsPerUnit = 6.0m },

                new IngredientConversion { Id = 8, IngredientId = 9, UnitId = 5, GramsPerUnit = 4.0m },

                new IngredientConversion { Id = 9, IngredientId = 10, UnitId = 4, GramsPerUnit = 213.0m }
            );

            // Decimal Precisions
            modelBuilder.Entity<IngredientConversion>()
                .Property(c => c.GramsPerUnit)
                .HasPrecision(18, 2);

            modelBuilder.Entity<RecipeIngredient>()
                .Property(c => c.Quantity)
                .HasPrecision(18, 2);

            // ADDED: Precision for the new GramWeight column
            modelBuilder.Entity<RecipeIngredient>()
                .Property(c => c.GramWeight)
                .HasPrecision(18, 2);
        }


    }
}
