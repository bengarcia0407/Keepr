<template>
  <div class="modal fade" id="CreateKeepForm" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
    aria-hidden="true">
    <div class="modal-dialog" role="document">
      <form class="modal-content" @submit.prevent="handlesubmit()">
        <div class="modal-header">
          <h3 class="modal-title" id="exampleModalLabel">Create Keep</h3>
          <div type="button" class="close" data-bs-dismiss="modal" aria-label="Close" title="close modal">
            <i class="mdi mdi-close mdi-36px text-danger" ></i>
          </div>
        </div>
        <div class="modal-body">
          <div class="form-group">
            <label for="title">Title</label>
            <input v-model="editable.name" type="text" class="form-control" id="title" name="title"
              placeholder="Enter Title" required title="new keep title">
          </div>
          <div class="form-group">
            <label for="Description">Description: Tell the story!</label>
            <textarea v-model="editable.description" class="form-control" id="description" placeholder="(optional)"
              name="description" rows="3" maxlength="500" title="new keep description"></textarea>
          </div>
          <div class="form-group">
            <label for="image">Image/Video</label>
            <input v-model="editable.img" type="text" class="form-control" id="image" placeholder="Paste link here!"
              name="image/video" maxlength="1000" title="new keep image" required>
          </div>
        </div>
        <div class="modal-footer">
          <button type="submit" class="btn btn-primary" name="submit button" title="submit form">Submit</button>
          <button type="button" class="btn btn-danger" data-bs-dismiss="modal" name="close button" title="close modal">Close</button>
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
            editable.value = {}
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
