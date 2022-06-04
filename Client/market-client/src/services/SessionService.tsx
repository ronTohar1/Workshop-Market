import { useNavigate } from "react-router-dom"
import { pathHome } from "../Paths"
import { serverEnter } from "./BuyersService"

const isInitOccured = "isInitOccured"

const isGuest = "isGuest"
const buyerId = "buyerId"

export async function initSession() {
  try {
    const didInit = localStorage.getItem(isInitOccured)
    if (didInit === null) {
      const response = serverEnter()
      const id = await response

      if (id.errorOccured) throw new Error(".......")

      initFields(id.value)
      console.log("initiated new guest id")
      alert("Hello, new guest!")
    }
  } catch (e) {
    alert("Sorry, dear visitor, an unkown error has occured!")
  }
}

function initFields(id: number) {
  localStorage.setItem(buyerId, String(id))
  localStorage.setItem(isGuest, "true")
  localStorage.setItem(isInitOccured, "true")
}

export function clearSession() {
  localStorage.clear()
}

function createGetter<T>(field: string): () => T {
  const val = localStorage.getItem(field)
  return () => (val === null ? val : JSON.parse(val))
}

function createSetter<T>(field: string): (v: T) => void {
  return (newValue: T) => localStorage.setItem(field, JSON.stringify(newValue))
}

// Guest setter Getter
export const getIsGuest: () => boolean = createGetter(isGuest)

export const setIsGuest: (v: boolean) => void = createSetter(isGuest)

// member setter Getter
export const getBuyerId: () => number = createGetter(buyerId)

export const setBuyerId: (v: number) => void = createSetter(buyerId)
