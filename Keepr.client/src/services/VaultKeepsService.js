import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { api } from "./AxiosService.js"
import { keepsService } from "./KeepsService.js"
import { vaultsService } from "./VaultsService.js"

class VaultKeepsService{

  async getVaultKeepById(id){
    const res = await api.get(`api/vaultKeeps/${id}`)
    logger.log('vault keep by id', res.data)
    return res.data
  }
  async removeKeepFromVault(id){
    const vaultKeep = await this.getVaultKeepById(id)
    if (AppState.user.id != vaultKeep.creatorId){
      throw new Error('unauthorized')
    }
    const yes = await Pop.confirm('Are you sure you want to remove this keep from your vault?')
    if (yes) {
      await api.delete(`api/vaultKeeps/${id}`)
      await vaultsService.getAllVaultKeeps(vaultKeep.vaultId)
      Pop.toast('Keep removed')
  }
}
async addToVault(vaultId, keepId){
  const vaultKeep = {}
  await vaultsService.getVaultById(vaultId)
  await keepsService.getKeepById(keepId)
  vaultKeep.keepId = keepId
  vaultKeep.vaultId = vaultId
  await api.post('api/vaultkeeps', vaultKeep)
  Pop.toast('Keep successfully placed in Vault.', "success")
}
}
export const vaultKeepsService = new VaultKeepsService()