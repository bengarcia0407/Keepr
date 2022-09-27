<template>
  <router-link :to="{name: 'Vault', params: {id:vault?.id}}">
    <div v-if="vault.id" class="image rounded elevation-3 m-3" :style="`background-image: url(${vault.img});`">
        <!-- <img class="cover" :src="vault.img" alt=""> -->
        <h4 class="p-2">
          {{vault.name}}
        </h4>
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
   height: 300px;
   background-size: cover;
   background-repeat: no-repeat;
   transition: transform .5s;
   &:hover{
     transform: scale(1.05);
   }
  }

h4 { 
   position: absolute; 
   top: 5px; 
   left: 10px; 
   width: 90%; 
   background-color: rgb(0, 0, 0, .5);
   text-shadow: 0px 2px 2px black;
}
</style>