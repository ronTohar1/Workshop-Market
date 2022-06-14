import Member from "../DTOs/Member"
import MemberInRole from "../DTOs/MemberInRole"
import Product from "../DTOs/Product"
import Roles from "../DTOs/Roles"
import Store from "../DTOs/Store"
import { fetchResponse } from "./GeneralService"
import { serverSearchProducts } from "./ProductsService"
import {
  serverGetStore,
} from "./StoreService"


export async function fetchStoresManagedBy(memberId: number): Promise<Store[]> {
  try {
    // Getting all stores ids from server
    const memberManager: MemberInRole = { memberId: memberId, roleInStore: Roles.Manager }
    const memberOwner: MemberInRole = { memberId: memberId, roleInStore: Roles.Owner }

    const managedStoresDict: Map<number, Product[]> = await fetchResponse(
      serverSearchProducts(null, null, null, null, null, null, memberManager, false) // Want all stores without products
    )

    const ownedStoresDict: Map<number, Product[]> = await fetchResponse(
      serverSearchProducts(null, null, null, null, null, null, memberOwner, false) // Want all stores without products
    )

    const managedStoresPromise: Promise<Store>[] = Object.keys(managedStoresDict).map((storeId: string) => fetchResponse(serverGetStore(Number(storeId))))
    const ownedStoresPromise: Promise<Store>[] = Object.keys(ownedStoresDict).map((storeId: string) => fetchResponse(serverGetStore(Number(storeId))))

    const memberStoresPromise: Promise<Store>[] = managedStoresPromise.concat(ownedStoresPromise)
    const memberStores: Promise<Store[]> = memberStoresPromise.reduce(
      async (stores: Promise<Store[]>, currStore: Promise<Store>) => (await stores).concat((await currStore))
      , Promise.resolve([]))
    console.log(memberStores)
    return memberStores
  } catch (e) {
    return Promise.reject(e)
  }

}

