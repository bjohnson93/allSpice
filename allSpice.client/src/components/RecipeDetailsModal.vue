<template>
  <div class="modal fade " id="recipeDetailModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl ">
    <div v-if="activeRecipe" class="modal-content">
      <div  class="modal-header">
          <h4 class="modal-title" id="exampleModalLabel">
            <span class="text-green me-3">{{activeRecipe?.title}}</span>  
            <span class="text-bubble-gray">{{ activeRecipe?.category }}</span></h4>
            <button type="button" class="btn-close btn " data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
      <div class="modal-body">
        <section class="row">
          <div class="col-4">
            <img class="img-fluid rounded" :src="activeRecipe?.img" alt="">
          </div>
            <!-- INSTRUCTIONS -->
            <div class="col-4 card bg-white px-0 ">
              <div class="fs-5 fs-md-4 mb-2 bg-green p-2 rounded d-flex justify-content-between">Recipe Steps
                <!-- DROPDOWN TO EDIT OR DELETE RECIPE -->
                <button type="button" v-if="account?.id == activeRecipe.creatorId" title="see options..." class="btn dropdown-toggle" data-bs-toggle="dropdown"><i class="mdi mdi-dots-horizontal "></i></button>
                <ul class="dropdown-menu p-1">
                  <li class="mb-1"><button title="Delete Recipe" @click="deleteRecipe(activeRecipe.id)" type="button" class="btn btn-danger">Delete Recipe<i class="mdi mdi-delete-outline"></i></button></li>
                  <!--*FIXME -  NEED TO FIX SO CREATE RECIPE TOGGLES, NEED EDIT FUNCTION ALSO. -->
                  <li ><button @click="setActiveRecipe(activeRecipe)" title="Edit Recipe" type="button" class="btn btn-warning" data-bs-toggle="modal" data-bs-target="#createRecipeModal">Edit Recipe<i class="mdi mdi-pencil"></i></button></li>
                </ul>
              </div>
              <p class="p-2">{{ activeRecipe?.instructions }}</p>
            </div>
            <!-- INGREDIENTS -->
            <div class="col-4 card bg-white px-0 ">
              <div class="fs-5 fs-md-4 mb-2 bg-green p-2 rounded d-flex justify-content-between">Ingredients
                <!-- DROPDOWN TO EDIT OR DELETE INGREDIENTS  -->
                <button type="button" v-if="account?.id == activeRecipe.creatorId" title="see options..." class="btn dropdown-toggle" data-bs-toggle="dropdown"><i class="mdi mdi-dots-horizontal "></i></button>
                <ul class="dropdown-menu p-1">
                  <li class="mb-1" @click="deleteIngredient()"><button title="Delete Ingredient..."  type="button" class="btn btn-danger">Delete Ingredient<i class="mdi mdi-delete-outline"></i></button></li>
                  <li><button @click="enableEditing()" title="Edit Ingredient" type="button" class="btn btn-warning">Edit Ingredient<i class="mdi mdi-pencil"></i></button></li>
                </ul>
              </div>
              <div v-for="i in ingredient" :key="i.id">
                <ul>
                  <!-- FORM TO EDIT INGREDIENTS -->
                  <form @submit.prevent="editIngredient(activeRecipe.id)">
                  <input :placeholder="i.quantity" v-model="editable.quantity" v-if="isEditing" minlength="1" maxlength="255">
                  <input :placeholder="i.name" v-model="editable.name" v-if="isEditing" minlength="1" maxlength="255">
                  <!-- END FORM, INGREDIENTS LISTED BELOW -->
                  <li class="justify-content-apart" v-else >
                      {{i.quantity}} {{ i.name }}
                    </li>
                    <button type="submit" v-if="isEditing" class="btn btn-success">Save Changes</button>
                </form>
                </ul>
              </div>
              <!-- FORM TO ADD INGREDIENT -->
              <div v-if="activeRecipe?.creatorId == account.id" >
                <form @submit.prevent="createIngredient()">
                  <div class="input-group mb-3">
                    <label class="mb-2 px-2 px-md-5" for="ingredient">Add Ingredient...</label>
                    <div class="mb-md-2 mb-1 px-2">
                      <input v-model="editable.name" type="text" class="form-control" placeholder=" Name" aria-label="Recipe ingredient" aria-describedby="basic-addon2" minlength="1" maxlength="255" required>
                    </div>
                    <div class="mb-md-2 mb-1 px-2">
                      <input v-model="editable.quantity" type="text" class="form-control" placeholder="Quantity" aria-label="Recipe ingredient" aria-describedby="basic-addon2" minlength="1" maxlength="255" required>
                    </div>
                    <div class="mb-md-2 mb-1 px-2">
                      <button title="Submit Ingredient..." class="btn btn-success"><i class="mdi mdi-plus-circle"></i></button>
                    </div>
                  </div>
                </form>
              </div>
            </div>
          </section>

          <section v-if="activeRecipe" class="mt-3 row d-flex justify-content-between">
            <div class="col-12 col-md-6 ">
              <button @click="createFavorite(activeRecipe)" class="btn fs-3 pt-0"> <i class="mdi mdi-heart-outline"></i></button>
            </div>
            <div class="col-12 col-md-6 text-end">
              <p>Published By {{ activeRecipe?.creator.name }}</p>
          </div>
          <!-- <p>{{ activeRecipeProp.id }}</p> -->
        </section>
      </div>

    </div>
    <div v-else class="d-flex justify-content-center align-items-center">
      <p class="fs-1 gray-box">Loading deliciousness... <i class="mdi mdi-food"></i></p></div>
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
import { recipesService } from "../services/RecipesService.js";
import { favoritesService } from "../services/FavoritesService.js";
import CreateRecipeModal from "./CreateRecipeModal.vue";


export default {
    // props: {
    //   activeRecipeProp: { type: Recipe }
    // },
    setup() {
        const editable = ref({});
        const isEditing = ref(false);
        watchEffect(() => {
            if (AppState.activeRecipe) {
                editable.value = { ...AppState.activeRecipe };
            }
        });
        return {
            editable,
            isEditing,
            activeRecipe: computed(() => AppState.activeRecipe),
            ingredient: computed(() => AppState.ingredients),
            account: computed(() => AppState.account),
            enableEditing() {
                isEditing.value = true;
                editable.value = { ...AppState.ingredients };
            },
            async createIngredient() {
                try {
                    const ingredientData = editable.value;
                    await ingredientsService.createIngredient(ingredientData);
                }
                catch (error) {
                    Pop.error(error.message);
                }
            },
            async editIngredient(recipeId){
              try 
              {
                const ingredientData = editable.value
                ingredientData.recipeId = recipeId
                await ingredientsService.editIngredient(ingredientData)
                isEditing.value = false
              }
              catch (error)
              {
                Pop.error(error.message)
              }
            },
            async deleteRecipe(recipeId) {
                try {
                    const wantsToDelete = await Pop.confirm("Deleting this recipe is permanent! Are you sure you want to delete?");
                    if (!wantsToDelete) {
                        return;
                    }
                    await recipesService.deleteRecipe(recipeId);
                }
                catch (error) {
                    Pop.error(error.message);
                }
            },
            async createFavorite(activeRecipe){
              try 
              {
                await favoritesService.createFavorite(activeRecipe)
              }
              catch (error)
              {
                Pop.error(error.message)
              }
            },
            setActiveRecipe(activeRecipe){
              recipesService.setActiveRecipe(activeRecipe)
            },
        };
    },
}
</script>


<style lang="scss" scoped>

</style>