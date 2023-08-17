using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace allSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;
  private readonly FavoritesService _favoritesService;

  public FavoritesController(Auth0Provider auth0Provider, FavoritesService favoritesService)
  {
    _auth0Provider = auth0Provider;
    _favoritesService = favoritesService;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Favorite>> CreateFavorite([FromBody] Favorite favoriteData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      favoriteData.AccountId = userInfo.Id;
      Favorite favorite = _favoritesService.CreateFavorite(favoriteData);
      return Ok(favorite);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{favoriteId}")]
  public ActionResult<Favorite> GetFavoriteById(int favoriteId)
  {
    try
    {
      Favorite favorite = _favoritesService.GetFavoriteById(favoriteId);
      return Ok(favorite);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{favoriteId}")]
  public async Task<ActionResult<string>> RemoveFavorite(int favoriteId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      _favoritesService.RemoveFavorite(favoriteId, userInfo.Id);
      return Ok("Favorite has been removed from your collection.");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
