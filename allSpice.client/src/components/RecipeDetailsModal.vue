<template>
  <div class="modal fade " id="recipeDetailModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl ">
    <div class="modal-content">
      <div class="modal-header">
          <h4 class="modal-title" id="exampleModalLabel">
            <span class="text-green me-3">{{activeRecipe?.title}}</span>  
            <span class="text-bubble-gray">{{ activeRecipe?.category }}</span></h4>
            
        <button type="button" class="btn-close btn btn-danger" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <section class="row">
          <div class="col-4">
            
            <img class="img-fluid rounded" :src="activeRecipe?.img" alt="">
          </div>

            <div class="col-4 card bg-white px-0">
              <p class="fs-4 bg-green p-2 rounded">Recipe Steps</p>
              <p>{{ activeRecipe?.instructions }}</p>
            </div>
            <div class="col-4 card bg-white px-0 ">
              <p class="fs-4 bg-green p-2 rounded">Ingredients</p>
              <!-- {{ ingredients }} -->
              <form @submit.prevent="createIngredient()">
                <div class="input-group mb-3">
                  <input v-model="editable.ingredients" type="text" class="form-control" placeholder="Add Ingredient to Recipe" aria-label="Recipe ingredient" aria-describedby="basic-addon2">
                  <span class="input-group-text selectable" id="basic-addon2"><i class="mdi mdi-plus-circle text-green"></i></span>
                </div>
              </form>
            </div>
        </section>
        <section class="mt-3 row d-flex justify-content-between">
          <div class="col-6">
              <button class="btn fs-3 pt-0"> <i class="mdi mdi-heart-outline"></i></button>
          </div>
          <div class="col-6 text-end">
            <p>Published By {{ activeRecipe?.creator.name }}</p>
          </div>
          <!-- <p>{{ activeRecipeProp.id }}</p> -->
        </section>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>
</template>


<script>
import { computed, onMounted, ref, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import Pop from "../utils/Pop.js";
import { ingredientsService } from "../services/IngredientsService.js";
import { Recipe } from "../models/Recipe.js";
import { logger } from "../utils/Logger.js";


export default {
  
  // props: {
  //   activeRecipeProp: { type: Recipe }
  // },
  setup(){
    const editable = ref({})
  //   async function getRecipeIngredients(){
  //   try 
  //   {
  //     const recipeId = props.activeRecipeProp.id
  //     // 
  //     // logger.log('recipe ingredients?? is this a recipe id:', recipeId)
  //     // await ingredientsService.getRecipeIngredients(recipeId)
  //   }
  //   catch (error)
  //   {
  //     Pop.error(error.message)
  //   }
  // }
  // onMounted(() => {
  //   getRecipeIngredients()
  // })
    // watchEffect(() => {
    //   AppState.activeRecipe
    // })

    return {
      editable,
      ingredients: computed(() => AppState.ingredients),
      activeRecipe: computed(() => AppState.activeRecipe),
      async createIngredient(){
        try 
        {
          const ingredientData = editable.value
          await ingredientsService.createIngredient(ingredientData)
        }
        catch (error)
        {
          Pop.error(error.message)
        }
      },
      // async createFavorite(){
      //   try 
      //   {
      //     const recipeId = activeRecipe.id
      //     await favoritesService.createFavorite(recipeId)
      //   }
      //   catch (error)
      //   {
      //     Pop.error(error.message)
      //   }
      // },
    } 
  }
}
</script>


<style lang="scss" scoped>

</style>