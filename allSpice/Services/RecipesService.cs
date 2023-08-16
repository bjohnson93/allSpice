using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allSpice.Services;

public class RecipesService
{
  private readonly RecipesRepository _recipesRepository;

  public RecipesService(RecipesRepository recipesRepository)
  {
    _recipesRepository = recipesRepository;
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    int recipeId = _recipesRepository.CreateRecipe(recipeData);

    Recipe recipe = GetRecipeById(recipeId);

    return recipe;
  }

  internal List<Recipe> GetRecipes()
  {
    List<Recipe> recipes = _recipesRepository.GetRecipes();
    return recipes;
  }

  public Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _recipesRepository.GetRecipeById(recipeId);

    if (recipe == null)
    {
      throw new Exception("There is no such recipe with that Id");
    }
    return recipe;
  }

  internal Recipe UpdateRecipe(int recipeId, Recipe recipeData, string userId)
  {
    Recipe originalRecipe = GetRecipeById(recipeId);

    if (originalRecipe.CreatorId != userId)
    {
      throw new Exception("NOT YOUR RECIPE TO DELETE!")
        }

    originalRecipe.Title = recipeData.Title ?? originalRecipe.Title;
    originalRecipe.Instructions = recipeData.Instructions ?? originalRecipe.Instructions;
    originalRecipe.Category = recipeData.Category ?? originalRecipe.Category;
    originalRecipe.Img = recipeData.Img ?? originalRecipe.Img;

    Recipe recipe = _recipesRepository.UpdateRecipe(originalRecipe);

    return recipe;
  }
}
