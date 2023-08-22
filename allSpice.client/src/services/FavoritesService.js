import { AppState } from "../AppState.js"
import { Favorite, FavoriteRecipe } from "../models/Favorite.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class FavoritesService {


  async getFavoriteRecipes() {
    const res = await api.get('/account/favorites')
    logger.log('[GETTING MY FAVS?]', res.data)

    AppState.favorites = res.data
  }

  async createFavorite(recipeId) {
    const res = await api.post('api/favorites', { recipeId })
    logger.log('Adding a favorite recipe...', res.data)
    const favRecipe = new Favorite(res.data)

    AppState.favorites.push(favRecipe)

  }
}

export const favoritesService = new FavoritesService()