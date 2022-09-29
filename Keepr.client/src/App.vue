<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <CreateKeepForm/>
</template>

<script>
import { computed } from 'vue';
import { AppState } from './AppState';
import CreateKeepForm from "./components/CreateKeepForm.vue";

export default {
    name: "App",
    setup() {
        return {
            appState: computed(() => AppState)
        };
    },
    components: { CreateKeepForm }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
</style>
