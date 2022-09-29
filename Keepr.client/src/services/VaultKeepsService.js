import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { api } from "./AxiosService.js"
import { vaultsService } from "./VaultsService.js"

class VaultKeepsService{

  async getVaultKeepById(id){
    const vaultKeep = await api.get(`api/vaultKeeps/${id}`)
    logger.log('vault keep by id', vaultKeep)
    return vaultKeep
  }
  async removeKeepFromVault(id){
    const vaultKeep = await this.getVaultKeepById(id)
    if (AppState.user.id != vaultKeep.creatorId){
      throw new Error('unauthorized')
    }
    const yes = await Pop.confirm('Are you sure you want to remove this keep from your vault?')
    if (yes) {
      await api.delete(`api/vaultKeeps/${id}`)
      const res = await vaultsService.getAllVaultKeeps(vaultKeep.vaultId)
      AppState.keeps = res.data
      Pop.toast('Keep removed')
  }
}
}
export const vaultKeepsService = new VaultKeepsService()