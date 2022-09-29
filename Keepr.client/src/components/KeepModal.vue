<template>
  <div class="modal fade modal-xl" id="keep-modal" tabindex="-1" aria-labelledby="keep-modalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header d-block">
          <div class="row">

            <img class="col-12 col-md-6 image-fluid draggable-none user-select-none" :src="keep.img" alt="keep image">

            <div class="col-12 col-md-6">
              <div class="row mt-0 me-3 ">
                <div class="col-3 align-self-end dropdown">
                  <h5 class="btn dropdown-toggle" type="button" id="dropdownMenuButton" title="select a vault" data-bs-toggle="dropdown"
                    aria-expanded="false"><i class="mdi mdi-plus-thick"></i> <b> to Vault</b></h5>
                  <div class="dropdown-menu" role="menu" aria-labelledby="dropdownMenuButton">
                    <div v-for="v in vaults" :key="v.id">
                      <h5 class="dropdown-item btn" role="menuitem" :title="v.name" @click="addToVault(v.id, keep.id)">{{v.name}}</h5>
                    </div>
                  </div>
                </div>
                <h5 class="col-2 offset-2 align-self-end"><i class="mdi mdi-eye"></i> {{keep.views}}</h5>
                <h5 class="col-2 align-self-end"><i class="mdi mdi-lock"></i> {{keep.kept}}</h5>
                <span id="modal-close" type="button" title="close button" class="mdi mdi-close mdi-36px col-1 offset-2 mt-0 pt-0"
                  data-bs-dismiss="modal"></span>
              </div>
              <h1 class="modal-title text-center" id="keep-modalLabel">{{keep.name}}</h1>
              <div class="modal-body">
                <p>{{keep.description}}</p>
              </div>

              <div v-if="user?.id == keep.creatorId" type="button" class="mdi mdi-delete-outline mdi-36px trash"
                @click="deleteKeep(keep.id)" title="delete keep"></div>
              <router-link v-if="keep.id" :to="{name: 'Profile', params: {id:keep?.creatorId}}" title="go to profile" class="profile-link">
                <div class="keep-modal-profile-img d-flex align-items-center" data-bs-dismiss="modal">
                  <img class="img-fluid rounded" :src="keep.creator.picture" alt="profile picture" :title="keep.creator.name">
                  <h5 class="ps-2 d-none d-md-block">{{keep.creator.name}}</h5>
                </div>
              </router-link>
              router or hide create buttons on profile page
              before unmount empty activevault
              create vault form needs work
              make form required, fix the ref for isPrivate


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
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {

  setup() {

    return {
      keep: computed(() => AppState.activeKeep),
      user: computed(() => AppState.user),
      vaults: computed(() => AppState.vaults),

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
      async addToVault(vaultId, keepId) {
        try {
          await vaultKeepsService.addToVault(vaultId, keepId)
        }
        catch (error) {
          logger.error(["adding keep to vault"], error)
          Pop.error(error);
        }
        document.getElementById("modal-close").click()
      },
    }
  }
}
</script>

<style lang="scss" scoped>
.keep-modal-profile-img {
  height: 45px;
  width: 45px;
  position: absolute;
  bottom: 3%;
  left: 50%;

  &:hover {
    transform: scale(1.002);
    transition: ease-in-out;
    filter: hue-rotate(60deg);
  }
}

.trash {
  color: dodgerblue;
  position: absolute;
  bottom: 2%;
  right: 1%;

  &:hover {
    transform: scale(1.01);
    transition: ease-in-out;
    filter: hue-rotate(125deg);
  }
}

h5 {
  color: dodgerblue;
}
</style>