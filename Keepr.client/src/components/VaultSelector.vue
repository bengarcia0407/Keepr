<template>

  <!-- this is a scrapped attempt to use vue to handle vault selection instead of bootstrap, I may return to this if bootstrap continues to have issues -->

              create vault form needs work
              make form required, fix the ref for isPrivate

</template>

<script>
export default {
setup(){
return{
  async addToVault(id) {
        try {
          
          await vaultKeepsService.addToVault(id);
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

</style>