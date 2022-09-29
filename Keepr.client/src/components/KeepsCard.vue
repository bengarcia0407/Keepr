<template>
  <div v-if="keep.id">
    <div  class="image rounded elevation-3">
      <img class="img-fluid" :src="keep.img" alt="keep card" type="button" @click="setActiveKeep(keep.id)">
      <h2 class="text-light ps-2">
        {{keep.name}}
      </h2>
      <i v-if="user.id && vault.creatorId == user.id" class="mdi mdi-delete mdi-36px remove-btn text-light" type="button" @click="removeKeepFromVault(keep.vaultKeepId)"></i>
      <router-link v-if="keep.creatorId != user.id" :to="{name: 'Profile', params: {id:keep?.creatorId}}">
        <img class="keep-profile-img img-fluid" :src="keep.creator.picture" alt="" :title="keep.creator.name">
      </router-link>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { Modal } from "bootstrap";
import { AppState } from "../AppState.js";
import { router } from "../router.js";
import { keepsService } from "../services/KeepsService.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import KeepModal from "./KeepModal.vue";

export default {
  props: {
    keep: { type: Object, required: true },
    user: { type: Object, required: false }
  },
  setup(props) {
    return {
      vault: computed(() => AppState.activeVault),
      async setActiveKeep(id) {
        try {
          await keepsService.setActiveKeep(id);
          await vaultsService.getAllProfileVaults(props.user.id)
          if (props.keep.creatorId != props.user.id) {
            props.keep.views++
          }
          Modal.getOrCreateInstance(document.getElementById("keep-modal")).toggle();
        }
        catch (error) {
          logger.error(["setting the active keep"], error)
          router.push({ name: 'Home' })
          Pop.error(error);
        }
      },
      async getKeepById(id) {
        try {
          await keepsService.getKeepById(id);
          if (props.keep.creatorId != props.user.id) {
            props.keep.views++
          }
        }
        catch (error) {
          logger.error(["getting keep by ID"], error)
          router.push({ name: 'Home' })
          Pop.error(error);
        }
      },
      async removeKeepFromVault(id) {
        try {
          await vaultKeepsService.removeKeepFromVault(id)
          }
        catch (error) {
          Pop.error(error);
        }
      },

    };
  },
  components: { KeepModal }
}
</script>

<style lang="scss" scoped>

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
  width: 100%;
  background-color: rgb(0, 0, 0, .25);
  text-shadow: 0px 2px 2px black;
}

.keep-profile-img {
  position: absolute;
  bottom: 5px;
  right: 10px;
  width: 45px;
  height: 45px;
  border-radius: 50%;

  &:hover {
    transform: scale(1.02);
    transition: ease-in-out;
    filter: contrast(40%);
  }
}

.remove-btn {
  position: absolute;
  bottom: 5px;
  left: 10px;
  text-shadow: 0px 1px 4px black;
  &:hover {
    transform: scale(1.02);
    transition: ease-in-out;
    filter: contrast(40%);
  }
}

.modal-img {
  background-size: contain;
  background-repeat: no-repeat;
  background-size: 80vh;
  @media (max-width: 756px) {
  background-size: 50vh;
  }
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
template{
  overflow-y: scroll;
}
</style>