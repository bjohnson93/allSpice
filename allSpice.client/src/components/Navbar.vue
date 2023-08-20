<template>
  <nav class="p-5 cool-nav"></nav>
  <nav class="navbar navbar-expand-lg navbar-dark bg-dark px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
            About
          </router-link>
        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
import Login from './Login.vue';
export default {
  setup() {
    return {}
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}
.cool-nav{
  background-image: url('https://images.unsplash.com/photo-1528404263917-f2bbc8593304?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NHx8aG9tZW1hZGV8ZW58MHx8MHx8fDA%3D&auto=format&fit=crop&w=500&q=60');
  background-repeat: no-repeat;
  object-fit: contain;
  object-position: center;
}

.navbar-nav .router-link-exact-active {
  /* border-bottom: 2px solid var(--bs-success); */
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
