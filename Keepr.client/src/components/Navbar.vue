<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-dark px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <h4 class="align-items-center"><img alt="logo" src="https://meltric.com/media/contentmanager/content/image_280.png" height="45" /> Keepr</h4>
      </div>
    </router-link>
    <button
      class="navbar-toggler"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon" />
    </button>
    <div class="collapse navbar-collapse justify-content-between" id="navbarText">
      <ul class="navbar-nav m-3">
        <li>
          <router-link v-if="userId"
            :to="{ name: 'Profile', params: {id: userId}}"
            class="btn text-primary lighten-30 selectable text-uppercase"
          > My Vaults
          </router-link>
        </li>
        <li>
          <button v-if="userId" @click="openCreateKeepForm()"
            class="btn text-primary lighten-30 selectable text-uppercase"
           title="create new keep"><i class="mdi mdi-plus-thick"></i> New keep
        </button>
        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
import { computed } from "@vue/reactivity";
import { Modal } from "bootstrap";
import { AppState } from "../AppState.js";

export default {
  setup() {
    return {
      userId: computed(()=> AppState.user.id),
      openCreateKeepForm(){
        Modal.getOrCreateInstance(document.getElementById("CreateKeepForm")).toggle();
      }
    };
  },
};
</script>

<style scoped>
a:hover {
  text-decoration: none;
}
.nav-link {
  text-transform: uppercase;
}
.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-primary);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
</style>
