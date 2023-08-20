import { AppState } from "../AppState.js"
import { Recipe } from "../models/Recipe.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class RecipesService {
  async setActiveRecipe(recipe) {
    AppState.activeRecipe = recipe
    // const recipeId = recipe.id
    // const res = await api.get('api/recipes/${recipeId}/ingredients')
    // logger.log('[ARE WE GETTING THE INGREDIENTS?]', res.data)
  }
  async getRecipes() {
    const res = await api.get('api/recipes')
    logger.log('[GOT RECIPES?]', res.data)

    AppState.recipes = res.data.map(rPojo => new Recipe(rPojo))
  }
  async createRecipe(recipeData) {
    const res = await api.post('api/recipes', recipeData)
    logger.log('creating a recipe....')
    const recipe = new Recipe(res.data)
    AppState.recipes.unshift(recipe)
    return recipe
  }

}

export const recipesService = new RecipesService()