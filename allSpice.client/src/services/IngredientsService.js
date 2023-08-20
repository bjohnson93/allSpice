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
    logger.log('ingredients from app state', AppState.ingredients)
  }

  async createIngredient(ingredientData) {
    const res = await api.post('api/ingredients', ingredientData)
    logger.log(res.data)
    AppState.ingredients = res.data.map(pojo => new Ingredient(pojo))
  }
}

export const ingredientsService = new IngredientsService()