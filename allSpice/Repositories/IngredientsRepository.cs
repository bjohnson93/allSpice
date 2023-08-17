using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allSpice.Repositories;

public class IngredientsRepository
{
  private readonly IDbConnection _db;

  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    string sql = @"
        INSERT INTO ingredients  (name, quantity, recipeId)
        VALUES (@Name, @Quantity, @RecipeId);
        SELECT 
        ing.*,
        rec.*
        FROM ingredients ing
        JOIN recipes rec ON ing.recipeId = rec.Id
        WHERE ing.id = LAST_INSERT_ID()
        ;";

    Ingredient ingredient = _db.Query<Ingredient, Recipe, Ingredient>(
      sql,
    (ingredient, recipe) =>
    {
      ingredient.RecipeId = recipe.Id;
      return ingredient;
    },
      ingredientData).FirstOrDefault();
    return ingredient;
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    string sql = @"
        SELECT
        ing.*, 
        rec.*
        FROM ingredients ing
        JOIN recipes rec WHERE ing.recipeId = @recipeId
        ;";

    List<Ingredient> ingredients = _db.Query<Ingredient, Recipe, Ingredient>(
      sql,
      (ingredient, recipe) =>
      {
        ingredient.RecipeId = recipeId;
        return ingredient;
      }
      ,
      new { recipeId }
      ).ToList();
    return ingredients;
  }

  internal void RemoveIngredient(int ingredientId)
  {
    string sql = "DELETE FROM ingredients WHERE id = @ingredientId LIMIT 1;";

    _db.Execute(sql, new { ingredientId });
  }

  // internal List<Ingredient> GetIngredients()
  // {
  //   string sql = @"
  //       SELECT 
  //       ing.*,
  //       rec.*
  //       FROM ingredients ing
  //       JOIN recipes rec ON rec.id = ing.recipeId
  //       ;";

  //   List<Ingredient> ingredients = _db.Query<Ingredient, Recipe, Ingredient>(
  //     sql,
  //     (ingredient, recipe) =>
  //     {
  //       ingredient.RecipeId = recipeId;
  //       return ingredient;
  //     }
  //   ).ToList();
  //   return ingredients;
  // }
}
