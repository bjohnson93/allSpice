using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allSpice.Services;

public class IngredientsService
{
  public readonly IngredientsRepository _ingredientsRepository;
  public readonly RecipesRepository _recipesRepository;

  public IngredientsService(IngredientsRepository ingredientsRepository, RecipesRepository recipesRepository)
  {
    _ingredientsRepository = ingredientsRepository;
    _recipesRepository = recipesRepository;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData, string userId)
  {
    Recipe recipe = _recipesRepository.GetRecipeById(ingredientData.RecipeId);

    if (recipe.CreatorId != userId)
    {
      throw new Exception("YOU CAN'T CREATE INGREDIENTS ON A RECIPE THAT'S NOT YOURS!");
    }

    Ingredient ingredient = _ingredientsRepository.CreateIngredient(ingredientData);

    return ingredient;
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    List<Ingredient> ingredients = _ingredientsRepository.GetIngredientsByRecipeId(recipeId);
    return ingredients;
  }

  internal void RemoveIngredient(int ingredientId)
  {
    _ingredientsRepository.RemoveIngredient(ingredientId);
  }




  // internal List<Ingredient> GetIngredients()
  // {
  //   List<Ingredient> ingredients = _ingredientsRepository.GetIngredients();
  //   return ingredients;
  // }
}
