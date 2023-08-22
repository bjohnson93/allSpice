<template>
<div class="container-fluid">

  <section class="row">
    <div class="col-12 p-3">

      <div class="text-end">
        <button title="Create Your Own Recipe" type="button" class="btn " @click="clearActiveRecipe()" data-bs-toggle="modal" data-bs-target="#createRecipeModal">
          <i class="mdi mdi-plus-circle text-green fs-1"></i>
        </button>
      </div>
    </div>
  </section>

  <section class="row justify-content-center align-items-center my-2">
    <div class="col-12 col-md-8 mb-2 rounded d-md-flex justify-content-center align-items-center text-center gray-box text-white">
      <button class="btn btn-outline-light mx-1 mb-2 mb-md-0" @click="filterBy = ''">All</button>
      <button class="btn btn-outline-light mx-1 mb-2 mb-md-0" @click="filterBy = 'Cheese'">Cheese</button>
      <button class="btn btn-outline-light mx-1 mb-2 mb-md-0" @click="filterBy = 'Italian'">Italian</button>
      <button class="btn btn-outline-light mx-1 mb-2 mb-md-0" @click="filterBy = 'Soup'">Soup</button>
      <button class="btn btn-outline-light mx-1 mb-2 mb-md-0" @click="filterBy = 'Specialty Coffee'">Specialty Coffee</button>
      <button class="btn btn-outline-light mx-1 mb-2 mb-md-0" @click="filterBy = 'Snacks'">Snacks</button>
      <button class="btn btn-outline-light mx-1 mb-2 mb-md-0" @click="filterBy = 'Mexican'">Mexican</button>
      <!-- *FIXME - Need to fix favorites button to filter through favorites.... -->
      <!-- <button class="btn btn-outline-light mx-1 mb-2 mb-md-0" @click="filterBy = 'favorites'">My Favorites</button> -->
    </div>

  </section>

  <section class="row p-3 justify-content-center align-items-center">
    <div class="col-md-4 col-12 my-3 m-auto" v-for="recipe in recipes" :key="recipe?.id">
      <!-- recipe card component -->
      <RecipeCard :recipeProp="recipe"/>
      
      <!-- <div class="mx-3 h-50 bg-white rounded recipeCard">
        <p class="fs-5 p-2">{{ recipe.category }}</p>
        <img class="img-fluid recipeImg rounded-bottom" :src="recipe.img" :alt="recipe.title">
        <p class="fs-4">{{ recipe.title }}</p>
      </div> -->
      <!-- end recipe card component -->
    </div>
  </section>
</div>



<!-- <RecipeDetailsModal :activeRecipeProp="activeRecipe"/> -->
</template>

<script>
import Pop from "../utils/Pop.js"
import { recipesService } from "../services/RecipesService.js"
import { logger } from "../utils/Logger.js"
import { computed, onMounted, ref, watchEffect } from "vue"
import { AppState } from "../AppState.js"
import { Recipe } from "../models/Recipe.js"
import { ingredientsService } from "../services/IngredientsService.js"
import { favoritesService } from "../services/FavoritesService.js"


export default {
  // props: { 
  //   recipe: {type: Recipe, required: true}},
  setup() {
    const filterBy = ref('')

    async function getRecipes(){
      try 
      {
      await recipesService.getRecipes()
      }
      catch (error)
      {
        Pop.error(error.message)
      }
    }
    async function getRecipeIngredients(recipeId){
      try 
      {
        await ingredientsService.getRecipeIngredients(recipeId)
      }
      catch (error)
      {
        Pop.error(error.message)
      }
    }
    watchEffect(() => {
      if (AppState.activeRecipe) {
        AppState.ingredients = []
        getRecipeIngredients(AppState.activeRecipe.id)
      }
    })
    onMounted(() => {
      getRecipes();
      // getFavoriteRecipes();
    });

    return {
      filterBy,
      // activeRecipe: computed(() => AppState.activeRecipe),
      // *FIXME - computed for favorites needs fixed
      favorites: computed(() => { 
        if(!filterBy.value){
          return AppState.recipes
        }
        return AppState.favorites.filter(favorite => favorite.recipeId == filterBy.value)
      }),
      recipes: computed(() => { 
        if(!filterBy.value){
          return AppState.recipes
        }
        return AppState.recipes.filter(recipe => recipe.category == filterBy.value)
      }),
      clearActiveRecipe() {
                recipesService.clearActiveRecipe();
            },
    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
  
}
</style>
