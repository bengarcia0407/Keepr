<template>
  <div>
    <div v-if="vault.id" class="image rounded elevation-3">
      <img class="img-fluid" :src="vault.img" alt="">
      <h2>
        {{vault.name}}
      </h2>
    </div>
  </div>
</template>

<script>
import { computed } from "vue";

export default {
  props: {
    vault: { type: Object, required: true }
  },
  setup() {
    return {
    }
  }
}
</script>

<style lang="scss" scoped>

.image { 
   position: relative; 
   width: 100%;
}

h2 { 
   position: absolute; 
   top: 5px; 
   left: 10px; 
   width: 90%; 
   background-color: rgb(0, 0, 0, .5);
   text-shadow: 0px 2px 2px black;
}
</style>