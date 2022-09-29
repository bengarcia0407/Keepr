<template>
  <router-link :to="{name: 'Vault', params: {id:vault?.id}}" title="go to vault">
    <div v-if="vault.id" class="image rounded elevation-3 m-3" :style="`background-image: url(${vault.img});`">
        <h6 class="p-2 text-light no-select">
          {{vault.name}}
        </h6>
      </div>
    </router-link>
</template>

<script>
import { computed } from "vue";

export default {
  props: {
    vault: { type: Object, required: true }
  },
  setup() {
    return {
      computed: {
        background(){
          return `background-image: url("vault.img");`;
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>

.image { 
   position: relative;
   height: 120px;
   width: 120px;
   background-size: cover;
   background-repeat: no-repeat;
   transition: transform .4s;
   &:hover{
     transform: scale(1.05);
   }
  }

h6 { 
   position: absolute; 
   bottom: 0px; 
   width: 100%; 
   background-color: rgb(0, 0, 0, .25);
   text-shadow: 0px 2px 2px black;
}
</style>