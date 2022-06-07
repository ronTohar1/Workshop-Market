import Member from "../DTOs/Member"
import Product from "../DTOs/Product"
import Roles from "../DTOs/Roles"
import Store from "../DTOs/Store"
import { fetchResponse } from "./GeneralService"
import { serverSearchProducts } from "./ProductsService"
import {
  dummyStore1,
  dummyStore2,
  serverGetMembersInRoles,
  serverGetStore,
} from "./StoreService"

export const dummyMember1 = new Member(0, "username1", true)

export async function fetchStoresManagedBy(memberId: number): Promise<Store[]> {
  try {
    // Getting all stores ids from server
    const storesToProducts: Map<number, Product[]> = await fetchResponse(
      serverSearchProducts(null, null, null, null, null, null)
    )

    // Create stores Ids array
    let storesIds: number[] = []
    for (const storeId in storesToProducts) {
      storesIds.push(Number(storeId))
    }

    // Filter stores that are managed or owned by member
    let managedStoresIds: number[] = []

    //Filtering for all stores that are Managed by member
    storesIds.forEach(async (storeId: number) => {
      try {
        const managers = await fetchResponse(
          serverGetMembersInRoles(memberId, storeId, Roles.Manager)
        )
        if (managers.includes(memberId)) managedStoresIds.push(storeId) // If member is manager in store -> add to managed stores
      } catch (e) {}
    })

    //Filtering for all stores that are owned by member
    storesIds.forEach(async (storeId: number) => {
      try {
        const managers = await fetchResponse(
          serverGetMembersInRoles(memberId, storeId, Roles.Owner)
        )
        if (managers.includes(memberId)) managedStoresIds.push(storeId) // If member is manager in store -> add to managed stores
      } catch (e) {}
    })

    // Get the managed or owned stores from server
    let managedStores: Store[] = []
    managedStoresIds.forEach(async (storeId: number) => {
      try {
        const store: Store = await fetchResponse(serverGetStore(storeId))
        managedStores.push(store)
      } catch (e) {}
    })

    return managedStores
  } catch (e) {
    return Promise.reject(e)
  }
  //   return fetchResponse(serverSearchProducts(null, null, null, null, null, null))
  //     .then((storesToProducts: Map<number, Product[]>) => {

  //       let storesIds: number[] = []
  //       for (const storeId in storesToProducts) {
  //         storesIds.push(Number(storeId))
  //       }

  //       let stores: Store[] = []
  //       storesIds.forEach((storeId: number) => {

  //         fetchResponse(
  //           serverGetMembersInRoles(member.id, storeId, Roles.Manager)
  //         )
  //           .then((managedStoresIds: number[]) => {
  //             if (managedStoresIds.includes(member.id)) {
  //               fetchResponse(serverGetStore(storeId)).then((store: Store) =>
  //                 stores.push(store)
  //               ).catch((e)=> Promise.reject(e))
  //             }
  //           })
  //       })
  //       return stores

  //     })
  //     .catch((e) => Promise.reject(e))
}
