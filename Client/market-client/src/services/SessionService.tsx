import { useNavigate } from "react-router-dom"
import { pathHome } from "../Paths"
import { serverEnter } from "./BuyersService"
import { fetchResponse } from "./GeneralService"

const storage = sessionStorage
const isInitOccured = "isInitOccured"

const isGuest = "isGuest"
const buyerId = "buyerId"

export async function initSession() {
  const didInit = storage.getItem(isInitOccured)
  while (didInit === null) {
    fetchResponse(serverEnter())
      .then((guestId: number) => {
        initFields(guestId)
        alert("Hello, new guest!")
      })
      .catch((e) => {
        alert("Sorry, dear visitor, an unkown error has occured!")
      })
  }
}

function initFields(id: number) {
  storage.setItem(buyerId, String(id))
  storage.setItem(isGuest, "true")
  storage.setItem(isInitOccured, "true")
}

export function clearSession() {
  storage.clear()
}

function createGetter<T>(field: string): () => T {
  const val = storage.getItem(field)
  return () => (val === null ? val : JSON.parse(val))
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
