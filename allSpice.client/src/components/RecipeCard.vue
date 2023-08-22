<template>
<div class="container fluid ">
  <section class="row ">
    
    <div :style="{'background-image':`url(${recipeProp?.img})`}" class="m-auto rounded recipeCard d-flex flex-column justify-content-between align-items-apart" >
      <div class="d-flex justify-content-between">
      <p class="fs-5 p-2 gray-box mt-3">{{ recipeProp.category }} </p>
      <button v-if="favorite.recipeId != recipeProp.id" @click="createFavorite(recipeProp?.id)" title="Add Recipe to Favorites" class="btn pt-0 fs-3 text-light"><i class="mdi mdi-heart-outline"></i></button>
    </div>
    <div title="See Recipe Details" class="selectable" data-bs-toggle="modal" data-bs-target="#recipeDetailModal" @click="setActiveRecipe()">
      <p class="fs-4 gray-box">{{ recipeProp.title }}</p>
    </div>
  
  </div>
  </section>
</div>


</template>


<script>
import { computed, onMounted, watchEffect } from "vue";
import { Recipe } from "../models/Recipe.js";
import { recipesService } from "../services/RecipesService.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { AppState } from "../AppState.js";
import { ingredientsService } from "../services/IngredientsService.js"
import { favoritesService } from "../services/FavoritesService.js"

export default {
  props: {
    recipeProp: {type: Recipe, required: true}
  },

  setup(props){
  watchEffect(() => {
    AppState.activeRecipe
  })
    return {
      async setActiveRecipe(){
        try 
        {
          await recipesService.setActiveRecipe(props.recipeProp)
          await ingredientsService.getRecipeIngredients(props.recipeProp.id)
        }
        catch (error)
        {
          Pop.error(error.message)
        }
      },
      async createFavorite(recipeId) {
        try 
        {
          await favoritesService.createFavorite(recipeId)
        }
        catch (error)
        {
          Pop.error(error.message)
        }
      },
      ingredients: computed(() => AppState.ingredients),
      account: computed(() => AppState.account),
      favorite: computed(() => AppState.favorites),
    }
  }
}
</script>


<style lang="scss" scoped>

  .recipeCard{
    height: 50vh;
    width: 100%;
    background-position: center;
    background-size: cover;
  }
      
</style>