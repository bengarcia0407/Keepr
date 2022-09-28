<template>
  <div class="">
    <div v-if="keep.id" class="image rounded elevation-3">
      <img class="img-fluid" :src="keep.img" alt="keep card" type="button" data-bs-toggle="modal"
        :data-bs-target="'#keep-modal-' + keep.id" @click="getKeepById(keep.id)">
      <h2>
        {{keep.name}}
      </h2>
      <router-link v-if="keep" :to="{name: 'Profile', params: {id:keep?.creatorId}}">
        <img class="keep-profile-img img-fluid" :src="keep.creator.picture" alt="" :title="keep.creator.name">
      </router-link>
    </div>
  </div>
  <Modal>
    <div class="modal fade modal-xl" :id="'keep-modal-' + keep.id" tabindex="-1" aria-labelledby="keep-modalLabel"
      aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header d-block">
            <div class="row">

              <!-- modal-img :style="`background-image: url(${keep.img});`" -->

              <img class="col-12 col-md-6 image-fluid draggable-none user-select-none width-50 object-fit"
                :src="keep.img" alt="keep image">


              <div class="col-12 col-md-6">
                <div class="row mt-0 me-3">
                  <span type="button" class="mdi mdi-close mdi-36px col-1 offset-11 mt-0 pt-0"
                    data-bs-dismiss="modal"></span>
                </div>
                <div class="d-flex d-inline align-items-center justify-content-center user-select-none">
                  <h5 class="m-3"><i class="mdi mdi-eye"></i> {{keep.views}}</h5>
                  <h5 class="m-3"><i class="mdi mdi-lock"></i> {{keep.kept}}</h5>
                </div>
                <h1 class="modal-title text-center" id="keep-modalLabel">{{keep.name}}</h1>
                <div class="modal-body">
                  <p>{{keep.description}}</p>
                </div>
                <div v-if="user?.id == keep.creatorId" type="button" class="mdi mdi-delete-outline mdi-36px"
                  @click="deleteKeep(keep.id)"></div>
                <router-link v-if="keep.id" :to="{name: 'Profile', params: {id:keep?.creatorId}}">
                  <div class="keep-modal-profile-img">
                    <img class="img-fluid" :src="keep.creator.picture" alt="" :title="keep.creator.name">
                    <p>{{keep.creator.name}}</p>
                  </div>
                </router-link>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

  </Modal>
</template>

<script>
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
      async deleteKeep(id) {
        try {
          await keepsService.deleteKeep(id);
        }
        catch (error) {
          logger.error(["deleting a keep"], error)
          document.getElementById("modal-close").click()
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