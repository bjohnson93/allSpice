using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allSpice.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int CreateFavorite(Favorite favoriteData)
  {
    string sql = @"
        INSERT INTO favorites (accountId, recipeId)
        VALUES (@AccountId, @RecipeId);
        SELECT LAST_INSERT_ID()
        ;";

    int favoriteId = _db.ExecuteScalar<int>(sql, favoriteData);

    return favoriteId;
  }

  internal Favorite GetFavoriteById(object favoriteId)
  {
    string sql = @"
        SELECT 
        fav.*,
        acc.*
        FROM favorites fav
        JOIN accounts acc ON acc.id = fav.accountId
        WHERE fav.id = @favoriteId LIMIT 1
        ;";

    Favorite favorite = _db.Query<Favorite, Profile, Favorite>(
      sql,
      (favorite, profile) =>
      {
        favorite.AccountId = profile.Id;

        return favorite;
      },
      new { favoriteId }).FirstOrDefault();
    return favorite;
  }

  internal List<RecipeFavorite> GetMyRecipeFavorites(string userId)
  {
    string sql = @"
        SELECT 
        fav.*,
        rec.*,
        acc.*   
        FROM favorites fav
        JOIN recipes rec ON rec.id = fav.recipeId
        JOIN accounts acc ON acc.id = rec.creatorId
        WHERE fav.accountId = @userId
        ;";

    List<RecipeFavorite> favorites = _db.Query<Favorite, RecipeFavorite, Profile, RecipeFavorite>(
      sql,
      (favorite, recipe, profile) =>
      {
        recipe.FavoriteId = favorite.Id;
        recipe.Creator = profile;
        return recipe;
      },
      new { userId }
      ).ToList();
    return favorites;
  }

  internal void RemoveFavorite(int favoriteId)
  {
    string sql = "DELETE FROM favorites WHERE id = @favoriteId LIMIT 1";

    int rowsAffected = _db.Execute(sql, new { favoriteId });

    if (rowsAffected > 1)
    {
      throw new Exception("CALL THE POLICE! WE'VE BEEN HACKED!");
    }
  }
}
