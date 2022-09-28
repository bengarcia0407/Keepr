<template>
    <div class="modal fade modal-xl" id="keep-modal" tabindex="-1" aria-labelledby="keep-modalLabel"
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
                  <span id="modal-close" type="button" class="mdi mdi-close mdi-36px col-1 offset-11 mt-0 pt-0"
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

</template>

<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {

  setup() {
    return {
      keep: computed(()=> AppState.activeKeep),
      user: computed(()=> AppState.user),

      async deleteKeep(id) {
        try {
          await keepsService.deleteKeep(id);
        }
        catch (error) {
          logger.error(["deleting a keep"], error)
          Pop.error(error);
        }
        document.getElementById("modal-close").click()
        await keepsService.getAllKeeps()
      },
    }
  }
}
</script>

<style lang="scss" scoped>

</style>