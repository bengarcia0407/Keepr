<template>
  <form>
    <div class="form-group">
      <label for="title">Title</label>
      <input type="text" class="form-control" id="title" name="title" placeholder="Enter Title" required >
    </div>
    <div class="form-group">
      <label for="Description">Description: Tell the story!</label>
      <textarea class="form-control" id="description" placeholder="(optional)" name="description" rows="3" maxlength="500"></textarea>
    </div>
    <div class="form-group">
      <label for="image">Image/Video</label>
      <input type="url" class="form-control" id="image" placeholder="Paste link here!" name="image/video" maxlength="1000">
    </div>
    <div class="form-check">
      <input type="checkbox" class="form-check-input" id="exampleCheck1">
      <label class="form-check-label" for="exampleCheck1">Check me out</label>
    </div>
    <button type="submit" class="btn btn-primary">Submit</button>
  </form>
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