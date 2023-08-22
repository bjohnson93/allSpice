<template>
  
  <nav class="p-5 cool-nav navbar-nav .router-link-exact-active">
    <div class="container-fluid">
      <Login />
        <section class="row d-flex justify-content-between">
          
          <div class="d-flex justify-content-center ">
            <p class="text-center text-light gray-box px-3 headingFont dropShadow">All Spice</p>
            <!-- <input class="rounded" title="Search Recipes..." placeholder="Search Recipes..." type="text">
              <div class="input-group-append"> -->
                <!-- </div> -->
              </div>
            </section>
            <section class="row d-flex  align-items-center justify-content-center py-4 ">
              <div class="col-4 d-flex justify-content-center ">
                <router-link class="navbar-brand d-flex text-light fs-bold me-5" :to="{ name: 'Home' }">
                  <button class="btn gray-box dropShadow">Recipes Home</button>
          </router-link> 

                <router-link class="navbar-brand d-flex text-light fs-bold" :to="{ name: 'Account' }">
                  <button class="btn gray-box dropShadow">My Favorite Recipes</button>
          </router-link> 
              </div>
      </section>
          </div>
  </nav>
  <!-- <nav class="navbar navbar-expand-lg navbar-dark bg-dark px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
    </router-link> -->
    <!-- <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li> -->
          <!-- <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
            About
          </router-link> -->
        <!-- </li>
      </ul> -->
      <!-- LOGIN COMPONENT HERE -->
    <!-- </div>
  </nav> -->
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
h1{
  width: fit-content;
}
a:hover {
  text-decoration: none;
}

.dropShadow{
  box-shadow: 2px 2px 10px whitesmoke;
}
.headingFont{
  font-size: 4em;
}

.navLinks{
  width: fit-content;
}
.nav-link {
  text-transform: uppercase;
}
.cool-nav{
  background-image: url('https://images.unsplash.com/photo-1564149504298-00c351fd7f16?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1170&q=80');
  background-size: cover;
  background-position: center;
}

.navbar-nav .router-link-exact-active {
  /* border-bottom: 2px solid var(--bs-success); */
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 250px;
  }
}
</style>
