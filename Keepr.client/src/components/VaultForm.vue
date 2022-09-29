<template>
  <div class="modal fade" id="VaultForm" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
    aria-hidden="true">
    <div class="modal-dialog" role="document">
      <form class="modal-content" @submit.prevent="handlesubmit()">
        <div class="modal-header">
          <h3 class="modal-title" id="exampleModalLabel">Create Vault</h3>
          <div type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
            <i class="mdi mdi-close mdi-36px text-danger"></i>
          </div>
        </div>
        <div class="modal-body">
          <div class="form-group">
            <label for="title">Vault Name</label>
            <input v-model="editable.name" type="text" class="form-control" id="title" name="title"
              placeholder="Enter Title" required>
          </div>
          <div class="form-group">
            <label for="Description">Vault Description</label>
            <textarea v-model="editable.description" class="form-control" id="description" placeholder="(optional)"
              name="description" rows="3" maxlength="500"></textarea>
          </div>
          <div class="form-group">
            <label for="image">Cover Image</label>
            <input v-model="editable.img" type="text" class="form-control" id="image" placeholder="Paste link here!"
              name="image/video" maxlength="1000">
          </div>
          <div class="form-check">
            <input v-model="editable.isPrivate" class="form-check-input" type="checkbox" value="" id="flexCheckDefault">
            <label class="form-check-label" for="flexCheckDefault">
              Private?
            </label>
          </div>
          <small class="text-dark lighten-50">Private Vaults can only be seen by you</small>
        </div>
        <div class="modal-footer">
          <button type="submit" class="btn btn-primary">Submit</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { Modal } from "bootstrap";
import { ref } from "vue";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    const editable = ref({})

    return {
      editable,
      async handlesubmit() {
        try {
          debugger
          await vaultsService.createVault(editable.value)
          Pop.toast('Vault created successfully!')
          editable.value = {}
          Modal.getOrCreateInstance(document.getElementById("VaultForm")).toggle()
          // }
        } catch (error) {
          logger.error('[edit/create Vault]', error);
          Pop.error(error);
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
