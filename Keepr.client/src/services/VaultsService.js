import { AppState } from "../AppState.js"
import { router } from "../router.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class VaultsService{

  async getVaultById(id){
    const res = await api.get(`api/vaults/${id}`)
    logger.log("get vault by id", res.data)
    if (res.data == null){
      // router.push({ name: 'Home'})
      throw new Error('no vault found with that Id')
    }
    return res.data
  }
  async getAllVaults(){
    const res = await api.get('api/vaults')
    // logger.log('getting all vaults', res.data)
    AppState.vaults = res.data
  }
  async getAllProfileVaults(id){
    const res = await api.get(`api/profiles/${id}/vaults`)
    logger.log('getting profile vaults', res.data)
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
}
export const vaultsService = new VaultsService()