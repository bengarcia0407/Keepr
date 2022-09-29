<template>
  <div class="modal fade" id="CreateKeepForm" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
    aria-hidden="true">
    <div class="modal-dialog" role="document">
      <form class="modal-content" @submit.prevent="handlesubmit()">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
          <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <div class="form-group">
            <label for="title">Title</label>
            <input v-model="editable.name" type="text" class="form-control" id="title" name="title"
              placeholder="Enter Title" required>
          </div>
          <div class="form-group">
            <label for="Description">Description: Tell the story!</label>
            <textarea v-model="editable.description" class="form-control" id="description" placeholder="(optional)"
              name="description" rows="3" maxlength="500"></textarea>
          </div>
          <div class="form-group">
            <label for="image">Image/Video</label>
            <input v-model="editable.img" type="text" class="form-control" id="image" placeholder="Paste link here!"
              name="image/video" maxlength="1000">
          </div>
        </div>
        <div class="modal-footer">
          <button type="submit" class="btn btn-primary">Submit</button>
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { Modal } from "bootstrap";
import { watchEffect, ref } from "vue";
// import { useRoute } from "vue-router";
// import { router } from "../router.js";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    const editable = ref({})
    // const router = useRoute()
    // watchEffect(() => {editable.value = {...props.keep}})
    return {
      editable,
      async handlesubmit() {
        try {
          // if (editable.value.id) {
          //   await keepsService.editKeep(editable.value)
          //   Pop.toast('keep edited successfully', "success")
          //   Modal.getOrCreateInstance(document.getElementById("CreateKeepForm")).toggle()
          // } else {
            await keepsService.createKeep(editable.value)
            Pop.toast('Keep created successfully!')
            Modal.getOrCreateInstance(document.getElementById("CreateKeepForm")).toggle()
          // }
        } catch (error) {
          logger.error('[edit/create keep]', error);
          Pop.error(error);
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
