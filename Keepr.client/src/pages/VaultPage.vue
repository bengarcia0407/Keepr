<template>
  <div class="container-fluid">
    <button v-if="user.id == vault.creatorId" class="delete-btn btn btn-primary" @click="deleteVault(user.id)" title="delete vault">Delete Vault</button>
    <div v-if="vault.id" class="row">
      <div class="profile-img col-6 col-md-3">
        <img :src="profile.picture" alt="profile picture" class="profile-image ms-5 my-5" name="profile-image"
          id="profile-image" title="profile-image">
      </div>
      <div class="col-10 col-md-8 mt-5 me-auto">
        <h1>{{profile?.name}}</h1>
        <h4>Keeps: {{keeps.length}}</h4>
      </div>
    </div>

    <div class="row">
      <div v-if="keeps.length" class="masonry-with-columns">
        <div v-for="k in keeps" :key="k.id">
          <KeepsCard :user="user" :keep="k" />
        </div>
      </div>
    </div>
  </div>
  <KeepModal />
</template>

<script>
import { vaultsService } from "../services/VaultsService.js"
import { onMounted, onUnmounted } from "vue";
import Pop from "../utils/Pop.js";
import KeepsCard from "../components/KeepsCard.vue";
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";
import { useRoute, useRouter } from "vue-router";
import KeepModal from "../components/KeepModal.vue";
import { keepsService } from "../services/KeepsService.js";

export default {
  name: "Profile",
  setup() {
    const route = useRoute()
    const router = useRouter()

    async function setActiveVault() {
      try {
        await vaultsService.setActiveVault(route.params.id);
      }
      catch (error) {
        Pop.error(error);
        router.push({ name: 'Home' })
      }
    }
    async function getAllVaultKeeps() {
      try {
        await vaultsService.getAllVaultKeeps(route.params.id);
        if (AppState.keeps.length == 0) {
          Pop.toast('This Vault is currently empty! Try adding some keeps from the home page.')
        }
      }
      catch (error) {
        Pop.error(error);
      }
    }
    async function clearActiveStates() {
      try {
        await keepsService.clearActiveStates();
      }
      catch (error) {
        Pop.error(error);
        router.push({ name: 'Home' })
      }
    }
    onMounted(() => {
      setActiveVault();
      getAllVaultKeeps();
    });
    onUnmounted(() => {
      clearActiveStates();
    })
    return {
      user: computed(() => AppState.user),
      keeps: computed(() => AppState.keeps),
      vault: computed(() => AppState.activeVault),
      profile: computed(() => AppState.activeProfile),
      async deleteVault(userId) {
      try {
        await vaultsService.deleteVault(route.params.id);
      }
      catch (error) {
        Pop.error(error);
      }
      router.push({ name: 'Profile', params: { id: userId } })
    }
    };
  },
  components: { KeepsCard, KeepModal }
}
</script>

<style scoped lang="scss">
.masonry-with-columns {
  columns: 20vw;
  column-gap: 1em;

  @media (max-width: 756px) {
    columns: 40vw;
  }

  .profile-img {
    @media (max-width: 756px) {
      offset: 3;
    }
  }

  div {
    display: inline-block;
    margin-bottom: 1em;
  }
}

.delete-btn {
  position: absolute;
  top: 15%;
  right: 1%;
  &:hover {
    transition: ease-in-out;
    filter: hue-rotate(150deg);
  }
}

template {
  position: relative;
}
</style>
