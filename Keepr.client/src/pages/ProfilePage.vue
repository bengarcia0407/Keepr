<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12">
        <div class="">
          <div v-for="v in vaults" :key="v.id">
            <VaultsCard :vault="v" />
          </div>
        </div>
        <div class="masonry-with-columns p-0 m-0">
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
import { onMounted } from "vue";
import Pop from "../utils/Pop.js";
import KeepsCard from "../components/KeepsCard.vue";
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";

export default {
  name: "Profile",
  setup() {
    async function getAllProfileVaults() {
      try {
        await keepsService.getAllProfileVaults();
      }
      catch (error) {
        Pop.error(error);
      }
    }
    async function getAllProfileKeeps() {
      try {
        await keepsService.getAllProfileKeeps();
      }
      catch (error) {
        Pop.error(error);
      }
    }
    onMounted(() => {
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
