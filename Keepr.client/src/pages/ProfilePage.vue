<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12">
        <div v-if="vaults.length" class="">
          <div v-for="v in vaults" :key="v.id">
            <VaultsCard :vault="v" />
          </div>
        </div>
        <div v-if="keeps.length" class="masonry-with-columns p-0 m-0">
          <div v-for="k in keeps" :key="k.id">
            <KeepsCard :keep="k" />
          </div>
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

    async function setActiveProfile(){
      try {
        await accountService.setActiveProfile(route.params.id);
      }
      catch (error) {
        Pop.error(error);
        router.push({name: 'Home'})
      }
    }
    async function getAllProfileVaults() {
      try {
        await vaultsService.getAllProfileVaults(route.params.id);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    async function getAllProfileKeeps() {
      try {
        await keepsService.getAllProfileKeeps(route.params.id);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    onMounted(() => {
      setActiveProfile();
      getAllProfileVaults();
      getAllProfileKeeps();
    });
    return {
      keeps: computed(() =>
        AppState.keeps),
      vaults: computed(() =>
        AppState.vaults)
    };
  },
  components: { KeepsCard, VaultsCard }
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
