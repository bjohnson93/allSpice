import { AppState } from "../AppState.js"
import { Ingredient } from "../models/Ingredient.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class IngredientsService {
  async getRecipeIngredients(recipeId) {
    const res = await api.get(`api/recipes/${recipeId}/ingredients`)
    logger.log('[ARE WE GETTING THE INGREDIENTS?]', res.data)
    const ingredients = res.data.map(ing => new Ingredient(ing))
    AppState.ingredients = ingredients
    // logger.log('ingredients from app state', AppState.ingredients)
  }

  async createIngredient(ingredientData) {
    ingredientData.recipeId = AppState.activeRecipe.id
    const res = await api.post('api/ingredients', ingredientData)
    AppState.ingredients.push(new Ingredient(res.data))
  }

  async editIngredient(ingredientData) {
    const res = await api.put(`api/ingredients/${ingredientData.recipeId}`, ingredientData)
    const ingredient = new Ingredient(res.data)
    const ingredientIndex = AppState.ingredients.find(i => i.id == ingredientData.id)
    AppState.ingredients.splice(ingredientIndex, 1, ingredient)
  }
}

export const ingredientsService = new IngredientsService()