<template>
  <div class="modal fade" id="CreateKeepForm" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
    aria-hidden="true">
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <form @submit="createKeep()">
            <div class="form-group">
              <label for="title">Title</label>
              <input type="text" class="form-control" id="title" name="title" placeholder="Enter Title" required>
            </div>
            <div class="form-group">
              <label for="Description">Description: Tell the story!</label>
              <textarea class="form-control" id="description" placeholder="(optional)" name="description" rows="3"
                maxlength="500"></textarea>
            </div>
            <div class="form-group">
              <label for="image">Image/Video</label>
              <input type="url" class="form-control" id="image" placeholder="Paste link here!" name="image/video"
                maxlength="1000">
            </div>
          </form>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
          <button type="submit" class="btn btn-primary">Submit</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  setup() {
    return {}
  }
}
</script>

<style lang="scss" scoped>

</style>
