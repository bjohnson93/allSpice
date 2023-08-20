<template>
<div class="container-fluid">
  <section class="row">
    <div class="col-12 p-3">
      <h1 class="text-center">All Spice</h1>
      <div class="text-end">

        <button type="button" class="btn " data-bs-toggle="modal" data-bs-target="#createRecipeModal">
    <i class="mdi mdi-plus-circle text-green fs-1"></i>
  </button>
      </div>
    </div>
  </section>
  <section class="row p-3 ">
    <div class="col-md-4 col-12 my-3 py-2 " v-for="recipe in recipes" :key="recipe?.id">
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


<CreateRecipeModal/>

<RecipeDetailsModal />
<!-- <RecipeDetailsModal :activeRecipeProp="activeRecipe"/> -->
</template>

<script>
import Pop from "../utils/Pop.js"
import { recipesService } from "../services/RecipesService.js"
import { logger } from "../utils/Logger.js"
import { computed, onMounted, watchEffect } from "vue"
import { AppState } from "../AppState.js"
import { Recipe } from "../models/Recipe.js"


export default {
  setup() {
    async function getRecipes(){
      try 
      {
        // logger.log('logging recipes soon...')
      await recipesService.getRecipes()
      }
      catch (error)
      {
        Pop.error(error.message)
      }
    }
    // watchEffect(() => {
    //   AppState.activeRecipe
    // })
    onMounted(() => {
      getRecipes();
    });

    return {
      // activeRecipe: computed(() => AppState.activeRecipe),
      recipes: computed(() => AppState.recipes)
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
