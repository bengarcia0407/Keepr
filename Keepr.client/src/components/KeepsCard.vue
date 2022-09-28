<template>
  <div class="">
    <div v-if="keep.id" class="image rounded elevation-3">
      <img class="img-fluid" :src="keep.img" alt="keep card" type="button" @click="setActiveKeep(keep.id)">
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
import { Modal } from "bootstrap";
import { router } from "../router.js";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import KeepModal from "./KeepModal.vue";

export default {
  props: {
    keep: { type: Object, required: true },
    user: { type: Object, required: true }
  },
  setup(props) {
    return {
      // user: computed(() => AppState.userInfo)
      // keepImg: `url(${props.keep.img})`
      async setActiveKeep(id){
        try {
          await keepsService.setActiveKeep(id);
          if(props.keep.creatorId != props.user.id){
            props.keep.views++}
            Modal.getOrCreateInstance(document.getElementById("keep-modal")).toggle();
        }
        catch (error) {
          logger.error(["setting the active keep"], error)
          router.push({ name: 'Home'})
          Pop.error(error);
        }
      },
      async getKeepById(id) {
        try {
          await keepsService.getKeepById(id);
          if(props.keep.creatorId != props.user.id){
            props.keep.views++
          }
        }
        catch (error) {
          logger.error(["getting keep by ID"], error)
          router.push({ name: 'Home'})
          Pop.error(error);
        }
      }
    };
  },
  components: { KeepModal }
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
  transition: transform .5s;

  &:hover {
    transform: scale(1.01);
  }
}

.d-inline {
  display: inline-block;
  margin-bottom: 1em;
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

.modal-img {
  background-size: contain;
  background-repeat: no-repeat;
  background-size: 80vh;
}

.modal-box {
  position: relative;
}

.width-50 {
  width: 50%
}

.object-fit {
  object-fit: contain;
}
</style>