<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>


  <CreateRecipeModal/>

<RecipeDetailsModal />

</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root{
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}


.dropShadow{
  box-shadow: 2px 2px 10px whitesmoke;
}

.text-green{
  color: rgb(73, 130, 73);
}
.text-bubble-gray{
  background-color: gray;
  padding: 1px 5px;
  border-radius: 10%;
  color: whitesmoke;
}
.bg-green{
  background-color: rgb(73, 130, 73);
  color: whitesmoke;
}
.gray-box{
  background: rgba(122, 117, 117, 0.72);
  border-radius: 16px;
  box-shadow: 0 4px 30px rgba(0, 0, 0, 0.1);
  backdrop-filter: blur(9.7px);
  -webkit-backdrop-filter: blur(9.7px);
  border: 1px solid rgba(122, 117, 117, 0.3);
  color: whitesmoke;
}
</style>
