import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class FavoritesService {

  async createFavorite(recipeId) {
    const res = await api.post('api/favorites')
    logger.log('Adding a favorite recipe...', res.data)
  }
}

export const favoritesService = new FavoritesService()