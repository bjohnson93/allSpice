<template>
  <div class="container-fluid">
    <section class="row mt-5">
      <div class="col-12">
        <div class="about text-center">
          <h1>Welcome {{ account.name }}</h1>
          <img class="rounded" :src="account.picture" alt="" />
          <p>{{ account.email }}</p>
        </div>
      </div>
    </section>
    <section class="row justify-content-center">
      <div class="col-6  text-center">
        <h1 class="gray-box dropShadow">My Favorite Recipes</h1>
      </div>
      <section class="row">

        <div v-for="fav in favorites" :key="fav.id" class="col-md-4 col-12 m-auto mb-3">
          <RecipeCard :recipeProp="fav"/>
        </div>
      </section>
    </section>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';
import Pop from "../utils/Pop.js";
import RecipeCard from "../components/RecipeCard.vue";
import { Favorite } from "../models/Favorite.js";
import { AuthService } from "../services/AuthService.js";
import { favoritesService } from "../services/FavoritesService.js";
export default {
    setup() {
        async function getFavoriteRecipes() {
            try {
                await favoritesService.getFavoriteRecipes();
            }
            catch (error) {
                Pop.error(error.message);
            }
        }
        onMounted(() => {
            getFavoriteRecipes();
        });
        return {
            account: computed(() => AppState.account),
            favorites: computed(() => AppState.favorites)
        };
    },
    components: { RecipeCard }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
