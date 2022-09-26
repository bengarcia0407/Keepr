import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class VaultsService{

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
}
export const vaultsService = new VaultsService()