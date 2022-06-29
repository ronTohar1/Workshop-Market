import Bid from "../DTOs/Bid"
import Member from "../DTOs/Member"
import MemberInRole from "../DTOs/MemberInRole"
import Product from "../DTOs/Product"
import Roles from "../DTOs/Roles"
import Store from "../DTOs/Store"
import { convergePromises, fetchResponse } from "./GeneralService"
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

    console.log("managedStores")
    console.log(managedStoresDict)

    const ownedStoresDict: Map<number, Product[]> = await fetchResponse(
      serverSearchProducts(null, null, null, null, null, null, memberOwner, false) // Want all stores without products
    )
    console.log("ownedStoresDict")
    console.log(ownedStoresDict)

    const managedStoresPromise: Promise<Store>[] = Object.keys(managedStoresDict).map((storeId: string) => fetchResponse(serverGetStore(Number(storeId))))
    const ownedStoresPromise: Promise<Store>[] = Object.keys(ownedStoresDict).map((storeId: string) => fetchResponse(serverGetStore(Number(storeId))))

    const memberStoresPromise: Promise<Store>[] = managedStoresPromise.concat(ownedStoresPromise)
    const memberStores: Promise<Store[]> = memberStoresPromise.reduce(
      async (stores: Promise<Store[]>, currStore: Promise<Store>) => (await stores).concat((await currStore))
      , Promise.resolve([]))
    return memberStores
  } catch (e) {
    return Promise.reject(e)
  }

}

export async function fetchBids(memberId: number): Promise<[Bid[], Product[]]> {
  try {
    // Getting all stores ids from server
    const memberManager: MemberInRole = { memberId: memberId, roleInStore: Roles.Manager }
    const memberOwner: MemberInRole = { memberId: memberId, roleInStore: Roles.Owner }

    const allStoresToProds: Map<number, Product[]> = await fetchResponse(
      serverSearchProducts(null, null, null, null, null, null, null, false) // Want all stores without products
    )

    const allStoresPromise: Promise<Store>[] = Object.keys(allStoresToProds).map((storeId: string) => fetchResponse(serverGetStore(Number(storeId))))

    const allStores: Promise<Store[]> = convergePromises(allStoresPromise)
    const BidsAndProducts: [Bid[], Product[]][] = (await allStores).map(s => [s.bids, s.products])
    const myBids: [Bid[], Product[]] = BidsAndProducts.reduce(
      (bidsProducts: [Bid[], Product[]], currBidsProducts: [Bid[], Product[]]) => {
        const bidsOfMember: Bid[] = currBidsProducts[0].filter(b => b.memberId === memberId)
        const products: Product[] = bidsOfMember.map((bid: Bid) => {
          return currBidsProducts[1].filter(p => p.id === bid.productId)[0]
        })
        return [bidsOfMember.concat(bidsProducts[0]), products.concat(bidsProducts[1])]
      }, [[], []]
    )
    return myBids
  } catch (e) {
    return Promise.reject(e)
  }

}

