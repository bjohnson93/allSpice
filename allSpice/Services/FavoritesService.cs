using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allSpice.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _favoritesRepository;

  public FavoritesService(FavoritesRepository favoritesRepository)
  {
    _favoritesRepository = favoritesRepository;
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    int favoriteId = _favoritesRepository.CreateFavorite(favoriteData);

    Favorite favorite = GetFavoriteById(favoriteId);

    return favorite;
  }

  public Favorite GetFavoriteById(int favoriteId)
  {
    Favorite favorite = _favoritesRepository.GetFavoriteById(favoriteId);
    if (favorite == null)
    {
      throw new Exception("There is no such ingredient with that Id");
    }
    return favorite;
  }

  internal List<RecipeFavorite> GetMyRecipeFavorites(string userId)
  {
    List<RecipeFavorite> favorites = _favoritesRepository.GetMyRecipeFavorites(userId);
    return favorites;
  }

  internal void RemoveFavorite(int favoriteId, string userId)
  {
    Favorite favorite = GetFavoriteById(favoriteId);

    if (favorite.AccountId != userId)
    {
      throw new Exception("NOT YOUR DATA!");
    }

    _favoritesRepository.RemoveFavorite(favoriteId);
  }
}
