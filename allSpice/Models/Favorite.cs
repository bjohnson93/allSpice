using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allSpice.Models;

public class Favorite
{
  public int Id { get; set; }
  public string AccountId { get; set; }
  public int RecipeId { get; set; }
}

public class FavoriteProfile : Profile
{
  public int FavoriteId { get; set; }
}

public class RecipeFavorite : Recipe
{
  public int FavoriteId { get; set; }

  // public int Id { get; set; }
  // public DateTime CreatedAt { get; set; }
  // public DateTime UpdatedAt { get; set; }
  // public string Title { get; set; }
  // public string Instructions { get; set; }
  // public string Img { get; set; }
  // public string Category { get; set; }
  // public string CreatorId { get; set; }

  // public Profile Creator { get; set; }

}


