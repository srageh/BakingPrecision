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
                recipe.Steps.Add(
                    new RecipeStep
                    {
                        StepNumber = recipeStep.StepNumber,
                        Instruction = recipeStep.Instruction

                    });
            }

        //    public int Id { get; set; }
        //public string Title { get; set; } = string.Empty;
        //public string? SourceUrl { get; set; }
        //public int? PrepTimeMinutes { get; set; }
        //public int? CookTimeMinutes { get; set; }
        //public string? Yield { get; set; }
        //public List<IngredientDTO> Ingredients { get; set; } = new();
        //public List<InstructionDto> Instructions { get; set; } = new();

            foreach(var ingrDTO in recipeDTO.Ingredients)
            {
                var ingredient = await _context.Ingredients.FirstOrDefaultAsync(x=> x.Name.ToLower() == ingrDTO.Name.ToLower());
                //Fetch or add new ingredient

                if(ingredient == null)
                {
                    var category = await _context.IngredientCategories.FirstOrDefaultAsync(x=> x.Name.ToLower() == ingrDTO.CategoryName.ToLower());
                    if(category == null)
                    {
                        category = await _context.IngredientCategories.FirstOrDefaultAsync(c => c.Name == "Other");

                        if (category == null)
                        {
                            category = new IngredientCategory { Name = "Other" };
                            _context.IngredientCategories.Add(category);
                            await _context.SaveChangesAsync();
                        }
                    }
                    ingredient = new Ingredient
                    {
                        Name = ingrDTO.Name,
                        IngredientCategoryId = category.Id
                    };
                    _context.Ingredients.Add(ingredient);
                    await _context.SaveChangesAsync();
                }

                var cleanUnitName = ingrDTO.Unit.ToLower().TrimEnd('s');
                //Fetch Unit
                var unit = await _context.Units.FirstOrDefaultAsync(u => 
                u.Abbreviation == cleanUnitName ||
                u.Name == cleanUnitName
                );

                //Conversions and Calculations 
                var conversion = await _context.IngredientConversions.FirstOrDefaultAsync(i => 
                i.IngredientId == ingredient.Id 
                && unit != null
                && i.UnitId == unit.Id
                );

                decimal conversionGramWeight;
                if (conversion != null)
                {
                    conversionGramWeight = ingrDTO.Quantity * conversion.GramsPerUnit;

                }
                else
                {
                    conversionGramWeight = ingrDTO.GramWeight;

                }

                recipe.RecipeIngredients.Add(new RecipeIngredient
                {
                    IngredientId = ingredient.Id,
                    UnitId = unit?.Id,
                    Quantity = ingrDTO.Quantity,
                    GramWeight = conversionGramWeight

                });



            }






            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            recipeDTO.Id = recipe.Id;

            return CreatedAtAction("GetRecipe", new { id = recipe.Id }, recipe);
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
