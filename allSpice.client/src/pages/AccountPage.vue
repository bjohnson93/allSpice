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
    <section class="row">
      <h1>My Favorite Recipes</h1>
      <div v-for="fav in favorites" :key="fav.id" class="col-3 m-auto mb-3">
        <RecipeCard :recipeProp="fav"/>
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';
import { accountService } from "../services/AccountService.js";
import Pop from "../utils/Pop.js";
import RecipeCard from "../components/RecipeCard.vue";
import { Favorite } from "../models/Favorite.js";
export default {
    setup() {
        async function getFavoriteRecipes() {
            try {
                await accountService.getFavoriteRecipes();
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
