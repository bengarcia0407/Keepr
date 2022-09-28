import { AppState } from "../AppState.js"
import { router } from "../router.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { api } from "./AxiosService.js"


class KeepsService{

  async getKeepById(id){
    const res = await api.get(`api/keeps/${id}`)
    if (res.data == null){
      router.push({ name: 'Home'})
      throw new Error('bad keep ID')
  }
  return res.data
  }
  async getAllKeeps(){
    const res = await api.get('api/keeps')
    // logger.log('getting all keeps', res.data)
    AppState.keeps = res.data
  }
  async getAllProfileKeeps(id){
    const res = await api.get(`api/profiles/${id}/keeps`)
    logger.log('getting profile keeps', res.data)
    AppState.keeps = res.data
  }
  async deleteKeep(id){
    const keep = await this.getKeepById(id)
    if (AppState.user.id != keep.creatorId){
      throw new Error('unauthorized')
    }
    const yes = await Pop.confirm('are you sure you want to permanently delete this keep? (this action cannot be undone!)')
    if (yes) {
      await api.delete(`api/keeps/${id}`)
      AppState.keeps.filter(k => k.id != id)
      Pop.toast('Keep deleted')
    }
  }
}
export const keepsService = new KeepsService()