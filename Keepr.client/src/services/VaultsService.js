import { AppState } from "../AppState.js"
import Pop from "../utils/Pop.js"
import { api } from "./AxiosService.js"


class VaultsService{

  async getVaultById(id){
    const res = await api.get(`api/vaults/${id}`)
    if (res.data == null){
      // router.push({ name: 'Home'})
      throw new Error('no vault found with that Id')
    }
    return res.data
  }
  async getAllVaults(){
    const res = await api.get('api/vaults')
    AppState.vaults = res.data
  }
  async getAllProfileVaults(id){
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.vaults = res.data
  }
  async getAllVaultKeeps(id){
    const res = await api.get(`api/vaults/${id}/keeps`)
    AppState.keeps = res.data
  }
  async setActiveVault(id){
    const vault = await this.getVaultById(id)
    AppState.activeVault = vault
  }
  async createVault(vaultData){
    const res = await api.post('api/vaults', vaultData)
    AppState.vaults.push(res.data)
  }
  async deleteVault(id){
    const vault = await this.getVaultById(id)
    if (AppState.user.id != vault.creatorId){
      throw new Error('unauthorized')
    }
    const yes = await Pop.confirm('Are you sure you want to permanently delete this Vault?')
    if (yes) {
      await api.delete(`api/Vaults/${id}`)
      AppState.vaults.filter(k => k.id != id)
      Pop.toast('Vault deleted')
    }
  }
}
export const vaultsService = new VaultsService()