using BakingPrecision.API.Data;
using BakingPrecision.API.DTOs;
using BakingPrecision.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakingPrecision.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeListDTO>>> GetRecipes()
        {
            return await _context.Recipes
                .Select(r => new RecipeListDTO
                {
                    Id = r.Id,
                    Title=r.Title,
                    PrepTimeMinutes = r.PrepTimeMinutes,
                    CookTimeMinutes = r.CookTimeMinutes

                })
                .ToListAsync();
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDTO>> GetRecipe(int id)
        {
            var recipeDTO = await _context.Recipes
                .Where(r => r.Id == id)
                .Select(r => new RecipeDTO
                {
                    Id = r.Id,
                    Title = r.Title,
                    Ingredients = r.RecipeIngredients.Select(i => new IngredientDTO
                    {
                        Name = i.Ingredient.Name,
                        Quantity = i.Quantity,
                       Unit = i.Unit != null ? i.Unit.Abbreviation : string.Empty,
                        CategoryName = i.Ingredient.Category != null ? i.Ingredient.Category.Name : "Other"

                    }
                    ).ToList(),
                    Instructions = r.Steps.Select(s => new InstructionDto
                    {
                        StepNumber = s.StepNumber,
                        Instruction = s.Instruction


                    }
                    ).OrderBy(s => s.StepNumber).ToList()


                }).FirstOrDefaultAsync();


            if (recipeDTO == null)
            {
                return NotFound();
            }

            return Ok(recipeDTO);
        }

        // PUT: api/Recipes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Recipes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RecipeDTO>> PostRecipe(RecipeDTO recipeDTO)
        {
            var recipe = new Recipe
            {
                Title = recipeDTO.Title,
                SourceUrl = recipeDTO.SourceUrl,
                PrepTimeMinutes = recipeDTO.PrepTimeMinutes,
                CookTimeMinutes = recipeDTO.CookTimeMinutes,
                Yield = recipeDTO.Yield,
                UserId = 1,
                Steps = new List<RecipeStep>(),
                RecipeIngredients = new List<RecipeIngredient>()
            };

            foreach (var recipeStep in recipeDTO.Instructions)
            {
                recipe.Steps.Add(new RecipeStep
                {
                    StepNumber = recipeStep.StepNumber,
                    Instruction = recipeStep.Instruction
                });
            }

            var otherCategory = await _context.IngredientCategories.FirstOrDefaultAsync(c => c.Name == "Other");

            foreach (var ingrDTO in recipeDTO.Ingredients)
            {
                var ingredient = await _context.Ingredients.FirstOrDefaultAsync(x => x.Name.ToLower() == ingrDTO.Name.ToLower());

                if (ingredient == null)
                {
                    var category = await _context.IngredientCategories.FirstOrDefaultAsync(x => x.Name.ToLower() == ingrDTO.CategoryName.ToLower());

                    if (category == null)
                    {
                        if (otherCategory == null)
                        {
                            otherCategory = new IngredientCategory { Name = "Other" };
                            _context.IngredientCategories.Add(otherCategory);
                        }
                        category = otherCategory;
                    }

                    ingredient = new Ingredient
                    {
                        Name = ingrDTO.Name,
                        Category = category // Matches your Ingredient.cs model perfectly
                    };

                    _context.Ingredients.Add(ingredient);
                }

                string cleanUnitName = null;
                if (!string.IsNullOrWhiteSpace(ingrDTO.Unit))
                {
                    var unitLower = ingrDTO.Unit.Trim().ToLower();
                    cleanUnitName = unitLower switch
                    {
                        "tablespoon" or "tablespoons" or "tbsps" or "tbsp" => "tbsp",
                        "teaspoon" or "teaspoons" or "tsps" or "tsp" => "tsp",
                        "cup" or "cups" or "c" => "cup",
                        "gram" or "grams" or "g" => "g",
                        "ounce" or "ounces" or "oz" => "oz",
                        "pound" or "pounds" or "lb" or "lbs" => "lb",
                        _ => unitLower
                    };
                }

                Unit unit = null;
                if (cleanUnitName != null)
                {
                    unit = await _context.Units.FirstOrDefaultAsync(u =>
                        u.Abbreviation == cleanUnitName || u.Name == cleanUnitName);
                }

                decimal conversionGramWeight = ingrDTO.GramWeight;

                if (ingredient.Id != 0 && unit != null)
                {
                    var conversion = await _context.IngredientConversions.FirstOrDefaultAsync(i =>
                        i.IngredientId == ingredient.Id && i.UnitId == unit.Id);

                    if (conversion != null)
                    {
                        conversionGramWeight = ingrDTO.Quantity * conversion.GramsPerUnit;
                    }
                }

                recipe.RecipeIngredients.Add(new RecipeIngredient
                {
                    Ingredient = ingredient,
                    Unit = unit,
                    Quantity = ingrDTO.Quantity,
                    GramWeight = conversionGramWeight
                });
            }

            _context.Recipes.Add(recipe);

            // Everything saves to the database right here in one clean transaction
            await _context.SaveChangesAsync();

            recipeDTO.Id = recipe.Id;

            return CreatedAtAction("GetRecipe", new { id = recipe.Id }, recipeDTO);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}
