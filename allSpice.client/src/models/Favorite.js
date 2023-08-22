import { Profile } from "./Account.js"
import { Recipe } from "./Recipe.js"

export class Favorite {
  constructor(data) {
    this.id = data.id
    this.accountId = data.accountId
    this.recipeId = data.recipeId
  }
}

export class FavoriteRecipe extends Recipe {
  constructor(data) {
    super(data)
    this.favoriteId = data.favoriteId
  }
}

export class FavoritesProfile extends Profile {
  constructor(data) {
    super(data)
    this.favoriteId = data.favoriteId
  }
}