import { useNavigate } from "react-router-dom"
import { pathHome } from "../Paths"
import { serverEnter } from "./BuyersService"
import { fetchResponse } from "./GeneralService"

interface Iuser {
  username: string
}

export const storage = sessionStorage
const isInitOccured = "isInitOccured"
const userName = "userName"
const isGuest = "isGuest"
const buyerId = "buyerId"
const isAdmin = "isAdmin"
const notificationsArr = "notifications"

export async function initSession() {
  if (storage.getItem(isInitOccured) === null) {
    storage.setItem(isInitOccured,"true")
    fetchResponse(serverEnter())
      .then((guestId: number) => {
        initFields(guestId)
        // alert("Hello, new guest!")
      })
      .catch((e) => {
        alert("Sorry, dear visitor, an unkown error has occured!")
      })
  }
}

function initFields(id: number) {
  storage.setItem(isInitOccured, "true")
  setBuyerId(id)
  setIsGuest(true)
  setUsername("guest")
  setIsAdmin(false)
}

export function clearSession() {
  storage.clear()
}

function createGetter<T>(field: string): () => T {
  const val = storage.getItem(field)
  return () => {
    if (val === null || val === undefined)
      return null
    else {
      return JSON.parse(val)
    }
  }
}

function createSetter<T>(field: string): (v: T) => void {
  return (newValue: T) => storage.setItem(field, JSON.stringify(newValue))
}

// Guest setter Getter
export const getIsGuest: () => boolean = createGetter(isGuest)

export const setIsGuest: (v: boolean) => void = createSetter(isGuest)

// member setter Getter
export const getBuyerId: () => number = createGetter(buyerId)

export const setBuyerId: (v: number) => void = createSetter(buyerId)

// username setter getter

export const getUsername: () => string = () => { return storage.getItem(userName) || "" }

export const setUsername: (v: string) => void = (v) => storage.setItem(userName, v)

// IsAdmin setter Getter
export const getIsAdmin: () => boolean = createGetter(isAdmin)

export const setIsAdmin: (v: boolean) => void = createSetter(isAdmin)
// notifications

// export const getNotifications: () => string[] = () => {
//   const notifications = storage.getItem(notificationsArr)
//   return notifications === null ? [] : JSON.parse(notifications)
// }

// export const addNotification: (v: string) => void = (v) => storage.setItem(userName, JSON.stringify(getNotifications().concat([v])))
// // export const removeNotification: (v: string) => void = (v) => storage.setItem(userName, v)
