<template>
  <div class="container-fluid">
    <div v-if="vault.id" class="row">
      <div class="col-3">
        <img :src="profile.picture" alt="profile picture" class="profile-image ms-5 my-5" name="profile-image"
          id="profile-image" title="profile-image">
      </div>
      <div class="col-8 mt-5 me-auto">
        <h1>{{profile.name}}</h1>
        <h4>Keeps: {{keeps.length}}</h4>
      </div>
    </div>

    <div class="row">
      <h2>Keeps <i class="btn mdi mdi-plus-outline mdi-36px"></i></h2>
      <div v-if="keeps.length" class="masonry-with-columns p-0 m-0">
        <div v-for="k in keeps" :key="k.id">
          <KeepsCard :keep="k" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { keepsService } from "../services/KeepsService.js"
import { vaultsService } from "../services/VaultsService.js"
import { onMounted } from "vue";
import Pop from "../utils/Pop.js";
import KeepsCard from "../components/KeepsCard.vue";
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";
import { useRoute, useRouter } from "vue-router";
import { accountService } from "../services/AccountService.js";
import VaultsCard from "../components/VaultsCard.vue";

export default {
  name: "Profile",
  setup() {
    const route = useRoute()
    const router = useRouter()

    async function setActiveVault() {
      try {
        await vaultsService.setActiveVault(route.params.id);
      }
      catch (error) {
        Pop.error(error);
        router.push({ name: 'Home' })
      }
    }
    async function getAllVaultKeeps() {
      try {
        await keepsService.getAllVaultKeeps(route.params.id);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    onMounted(() => {
      setActiveVault();
      getAllVaultKeeps();
    });
    return {
      keeps: computed(() => AppState.keeps),
      vault: computed(() => AppState.activeVault)
    };
  },
  components: { KeepsCard }
}
</script>

<style scoped lang="scss">
.masonry-with-columns {
  columns: 25vw;
  column-gap: 1em;

  div {
    display: inline-block;
    margin-bottom: 1em;
  }
}
</style>
