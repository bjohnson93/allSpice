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
    AppState.recipes.push(recipe)
  }

  async editRecipe(recipeData) {
    const res = await api.put(`api/recipes/${recipeData.id}`, recipeData)
    const recipe = new Recipe(res.data)
    AppState.activeRecipe = recipe
    // const recipeIndex = AppState.recipes.find(r => r.id == recipeData.id)
    // AppState.recipes.splice(recipeIndex, 1, recipe)
  }

  async deleteRecipe(recipeId) {
    const res = await api.delete(`api/recipes/${recipeId}`)
    logger.log('[Deleting recipe...]', res.data)
    AppState.recipes = AppState.recipes.filter(recipe => recipe.id != recipeId)
  }

  clearActiveRecipe() {
    AppState.activeRecipe = null
  }

}

export const recipesService = new RecipesService()