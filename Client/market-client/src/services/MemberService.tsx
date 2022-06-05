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
    const managedStoresIds: number[] = await filterStoresManagedBy(
      memberId,
      storesIds
    )

    console.log("managedStoresIds\n")
    console.log(managedStoresIds)

    // Get the managed or owned stores from server

    const managedStores: Promise<Store[]> = managedStoresIds.reduce(
      async (prevStores: Promise<Store[]>, storeId: number) => {
        return fetchResponse(serverGetStore(storeId))
          .then(async (store: Store) => (await prevStores).concat(store))
          .catch(async (e) => prevStores)
      },
      Promise.resolve<Store[]>([])
    )

    console.log("managedStores")
    console.log(managedStores)
    console.log("memberId")
    console.log(memberId)
    return managedStores
  } catch (e) {
    alert("rejecting")
    return Promise.reject(e)
  }
}

const filterStoresManagedBy = async (
  memberId: number,
  storesIds: number[]
): Promise<number[]> => {
  // Filter stores that are managed or owned by member

  //Filtering for all stores that are Managed by member
  const ownedStoresIds = await storesIds.reduce(
    reduceStoreByMember(memberId, Roles.Owner),
    Promise.resolve<number[]>([])
  )

  //Filtering for all stores that are owned by member
  const managedStoresIds = await storesIds.reduce(
    reduceStoreByMember(memberId, Roles.Manager),
    Promise.resolve<number[]>([])
  )

  return managedStoresIds.concat(ownedStoresIds)
}

// Reduces a store for a specific member
const reduceStoreByMember = (memberId: number, role: Roles) => {
  return (membersInRole: Promise<number[]>, storeId: number) =>
    reduceSingleStore(memberId, membersInRole, storeId, role)
}

//
const reduceSingleStore = async (
  memberId: number,
  previousStores: Promise<number[]>,
  storeId: number,
  role: Roles
): Promise<number[]> => {
  return fetchResponse(serverGetMembersInRoles(memberId, storeId, role))
    .then(async (owners: number[]) => {
      console.log("owner")
      console.log(owners)

      if (owners.includes(memberId))
        return (await previousStores).concat([storeId])
      else return previousStores
    })
    .catch((e) => {
      return previousStores
    })
}
