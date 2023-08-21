import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { Favorite } from "../models/Favorite.js"
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getFavoriteRecipes() {
    const res = await api.get('/account/favorites')
    logger.log('[GETTING MY FAVS?]', res.data)
    AppState.favorites = res.data
  }
}

export const accountService = new AccountService()
