<template>

<!-- Modal RECIPE FORM -->
<div class="modal fade" id="createRecipeModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header bg-green">
            <h5>New Recipe</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <div class="container-fluid">

          <form @submit.prevent="handleSubmit()" action="">
            <div class="mb-3">
              <label for="">Title</label>
              <input title="Recipe Title" v-model="editable.title" class="form-control" type="text" minlength="3" maxlength="255" required>
            </div>
            <div>
              <select title="Select Category" v-model="editable.category" class="form-select form-control" aria-label="Default select example">
                <!-- <option selected>Category</option> -->
                <option title="Cheese" editable.category="Cheese">Cheese</option>
                <option title="Italian" editable.category="Italian">Italian</option>
                <option title="Soup" editable.category="Soup">Soup</option>
                <option title="Mexican" editable.category="Mexican">Mexican</option>
                <option title="Specialty Coffee" editable.category="Specialty Coffee">Specialty Coffee</option>
                <option title="Snacks" editable.category="Snacks">Snacks</option>
              </select>
            </div>
            <div>
              <label for="">Instructions</label>
              <input title="Instructions..." v-model="editable.instructions" class="form-control" type="text" minlength="3" maxlength="255" required>
            </div>
            <div class="mb-3">
              <label for="">Image</label>
              <input title="Image..." v-model="editable.img" class="form-control" type="url" minlength="3" maxlength="700" required>
            </div>
            <button type="submit" title="Save Recipe" class="btn bg-green">Save Recipe!</button>
          </form>

        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" title="Close Form" data-bs-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>
</template>


<script>
import { computed, ref, watchEffect } from "vue";
import Pop from "../utils/Pop.js";
import { recipesService } from "../services/RecipesService.js";
import { logger } from "../utils/Logger.js";
import { AppState } from "../AppState.js";
import { Modal } from "bootstrap";

export default {
  setup(){
    const editable = ref({})
    watchEffect(() => {
      editable.value = {...AppState.activeRecipe}
    })
    return {
      recipe: computed(() => AppState.recipe),
      editable,
      handleSubmit(){
        if(editable.value.id){
          this.editRecipe()
        }
        else{
          editable.value = {}
          this.createRecipe()
        }
      },
      async createRecipe(){
        try 
        {
          const recipeData = editable.value
          logger.log('[CREATING A RECIPE...]', recipeData)
          await recipesService.createRecipe(recipeData)
          editable.value = {}
          Modal.getOrCreateInstance('#createRecipeModal').hide()
        }
        catch (error)
        {
          Pop.error(error.message)
        }
      },
      async editRecipe(){
        try 
        {
          const recipeData = editable.value
          await recipesService.editRecipe(recipeData)
          editable.value = {}
          Modal.getOrCreateInstance('#createRecipeModal').hide()
        }
        catch (error)
        {
          Pop.error(error.message)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>

</style>