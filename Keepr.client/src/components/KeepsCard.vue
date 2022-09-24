<template>
  <div>
    <div v-if="keep.id" class="image rounded elevation-3">
      <img class="img-fluid" :src="keep.img" alt="">
      <h2>
        {{keep.name}}
      </h2>
    </div>
  </div>
</template>

<script>
import { computed } from "vue";

export default {
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    return {
      // keepImg: `url(${props.keep.img})`
    }
  }
}
</script>

<style lang="scss" scoped>
// .keep-img-bg {
//   // background-image: v-bind(keepImg);
//   background-position: center;
//   background-size: contain;
//   background-repeat: no-repeat;
//   width:auto;
//   height: 100%;
// }

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