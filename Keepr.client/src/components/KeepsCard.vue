<template>
  <div>
    <div v-if="keep.id" class="image rounded elevation-3">
      <img class="img-fluid" :src="keep.img" alt="">
      <h2>
        {{keep.name}}
      </h2>
      <router-link v-if="keep" :to="{name: 'Profile', params: {id:keep?.creatorId}}">
        <img class="keep-profile-img img-fluid" :src="keep.creator.picture" alt="" :title="keep.creator.name">
      </router-link>
    </div>
  </div>
</template>

<script>
// import { computed } from "vue";

export default {
  props: {
    keep: { type: Object, required: true }
  },
  setup() {
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

.keep-profile-img { 
   position: absolute; 
   bottom: 5px; 
   right: 10px; 
   width: 45px;
   height: 45px;
   border-radius: 50%;
}
</style>