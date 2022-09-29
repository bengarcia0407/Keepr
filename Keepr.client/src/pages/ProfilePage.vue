<template>
  <div class="container-fluid">
    <div v-if="profile.id" class="row">
      <div class="col-10 col-md-2">
        <img :src="profile.picture" alt="profile picture" class="profile-image ms-5 my-5" name="profile-image"
          id="profile-image" title="profile-image">
      </div>
      <div class="col-10 col-md-8 mt-5 me-auto">
        <h1>{{profile.name}}</h1>
        <h4>Vaults: {{vaults.length}}</h4>
        <h4>Keeps: {{keeps.length}}</h4>
      </div>
    </div>
    <h2>Vaults <i v-if="user.id" class="btn mdi mdi-plus-outline mdi-36px" @click="openVaultForm()" title="create new vault"></i></h2>
    <div v-if="vaults.length" class="row">
      <div v-for="v in vaults" :key="v.id" class="col-4 col-md-1 m-3">
        <VaultsCard :vault="v" />
      </div>
    </div>
    <div class="row">
      <h2>Keeps <i v-if="user.id" class="btn mdi mdi-plus-outline mdi-36px" @click="openCreateKeepForm()" title="create new keep"></i></h2>
      <div v-if="keeps.length" class="masonry-with-columns">
        <div v-for="k in keeps" :key="k.id">
          <KeepsCard :user="user" :keep="k" />
        </div>
      </div>
    </div>
  </div>
  <KeepModal/>
  <VaultForm/>
</template>

<script>
import { keepsService } from "../services/KeepsService.js"
import { vaultsService } from "../services/VaultsService.js"
import { onMounted } from "vue";
import Pop from "../utils/Pop.js";
import KeepsCard from "../components/KeepsCard.vue";
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";
import { useRoute, useRouter } from "vue-router";
import { accountService } from "../services/AccountService.js";
import VaultsCard from "../components/VaultsCard.vue";
import KeepModal from "../components/KeepModal.vue";
import CreateKeepForm from "../components/CreateKeepForm.vue";
import { Modal } from "bootstrap";
import VaultForm from "../components/VaultForm.vue";

export default {
  name: "Profile",
  setup() {
    const route = useRoute()
    const router = useRouter()

    async function setActiveProfile() {
      try {
        await accountService.setActiveProfile(route.params.id);
      }
      catch (error) {
        Pop.error(error);
        router.push({ name: 'Home' })
      }
    }
    async function getAllProfileVaults() {
      try {
        await vaultsService.getAllProfileVaults(route.params.id);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    async function getAllProfileKeeps() {
      try {
        await keepsService.getAllProfileKeeps(route.params.id);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    onMounted(() => {
      setActiveProfile();
      getAllProfileVaults();
      getAllProfileKeeps();
    });
    return {
      user: computed(() => AppState.user),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      profile: computed(() => AppState.activeProfile),
      openCreateKeepForm(){
        Modal.getOrCreateInstance(document.getElementById("CreateKeepForm")).toggle();
      },
      openVaultForm(){
        Modal.getOrCreateInstance(document.getElementById("VaultForm")).toggle();
      }
    };
  },
  components: { KeepsCard, VaultsCard, KeepModal, CreateKeepForm, VaultForm }
}
</script>

<style scoped lang="scss">
.masonry-with-columns {
  columns: 15vw;
  column-gap: 1em;
  @media (max-width: 756px) {
  columns: 40vw;
  }
  div {
    display: inline-block;
    margin-bottom: 1em;
  }
}
.profile-image{
  height: 150px;
  width: 150px;
  border-radius: 5%;
}
</style>
