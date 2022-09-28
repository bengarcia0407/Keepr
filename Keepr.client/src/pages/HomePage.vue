<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12">
        <div v-if="keeps.length" class="masonry-with-columns p-0 m-0">
          <div v-for="k in keeps" :key="k.id">
            <KeepsCard :user="user" :keep="k"/>
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
import { logger } from "../utils/Logger.js";

export default {
  name: "Home",
  setup() {
    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps();
      }
      catch (error) {
        logger.error(["getting all keeps"], error)
        Pop.error(error);
      }
    }
    onMounted(() => {
      getAllKeeps();
    });
    return {
      keeps: computed(() => AppState.keeps),
      user: computed(() => AppState.user)
    };
  },
  components: { KeepsCard }
}
</script>

<style scoped lang="scss">
.masonry-with-columns {
  columns: 20vw;
  column-gap: 1em;

  div {
    display: inline-block;
    margin-bottom: 1em;
  }
}
</style>
