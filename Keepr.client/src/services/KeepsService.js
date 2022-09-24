import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class KeepsService{

  async getAllKeeps(){
    const res = await api.get('api/keeps')
    // logger.log('getting all keeps', res.data)
    AppState.keeps = res.data
  }
}
export const keepsService = new KeepsService()