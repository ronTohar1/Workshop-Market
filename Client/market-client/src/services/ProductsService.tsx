import Cart from "../DTOs/Cart"
import Product from "../DTOs/Product"
import ShoppingBag from "../DTOs/ShoppingBag"
import Store from "../DTOs/Store"
import { IDictionary, serverPort } from "../Utils"
import ClientResponse from "./Response"
import { serverGetStore } from "./StoreService"

// Return an array of [Products ids, Product -> quantity map from cart]
export const getCartProducts = (
  cart: Cart
): readonly [number[], Map<number, number>] => {
  const prodsIds = []
  const prodsToQuantity: Map<number, number> = new Map()
  // Getting all products ids in order to fetch the products from the server
  if (cart != null && cart.shoppingBags !== undefined) {
    const shoppingBags: any = cart.shoppingBags //Store id to shopping bag
    for (const storeId in shoppingBags) {
      const shoppingBag: ShoppingBag = shoppingBags[Number(storeId)]
      const productsAmounts: any = shoppingBag.productsAmounts // Product id to amount in cart
      for (const prodIdString in productsAmounts) {
        const prodId = Number(prodIdString)
        prodsToQuantity.set(prodId, productsAmounts[prodId])
        prodsIds.push(prodId)
      }
    }
  }
  return [prodsIds, prodsToQuantity] as const // Array of [product ids, Product to quantity map]
}
// should be async
export const fetchProducts = async (
  storeToProdPromise: Promise<ClientResponse<Map<number, Product[]>>>
): Promise<Product[]> => {
  try {
    const serverResponse = await storeToProdPromise

    const productsByStore: any = serverResponse.value // Dicitionary of store to products
    if (serverResponse.errorOccured) {
      alert("Whoops! " + serverResponse.errorMessage)
      return []
    }

    let allProducts: Product[] = []

    for (const storeId in productsByStore) {
      const prodsOfStore: Product[] = productsByStore[Number(storeId)] // Products of store (Product[] type)
      allProducts = allProducts.concat(prodsOfStore)
    }
    const products = allProducts
    return products
  } catch (e) {
    alert("Sorry but an unknown error happend when tried to search products")
    return []
  }
}

export const fetchProductsByStore = async (
  storeToProdPromise: Promise<ClientResponse<Map<number, Product[]>>>
): Promise<Product[][]> => {
  try {
    const serverResponse = await storeToProdPromise

    const productsByStore: any = serverResponse.value // Dicitionary of store to products
    if (serverResponse.errorOccured) {
      alert("Whoops! " + serverResponse.errorMessage)
      return []
    }

    const allProducts: Product[][] = []

    for (const storeId in productsByStore) {
      const prodsOfStore: Product[] = productsByStore[Number(storeId)] // Products of store (Product[] type)
      if (prodsOfStore.length > 0) allProducts.push(prodsOfStore)
    }
    return allProducts
  } catch (e) {
    alert("Sorry but an unknown error happend when tried to search products")
    return []
  }
}

// const takeMapValues = (prodsMap: Map<number,Product[]>) : Product[][] => {
//   if(prodsMap.)

//   return [[]]

// }

export function groupByStore(products: Product[]): Product[][] {
  const groupedProductsMap: Map<number, Product[]> = new Map()
  products.forEach((prod: Product) => {
    if (groupedProductsMap.has(prod.storeId)) {
      const prodArr = groupedProductsMap.get(prod.storeId)
      const arrToAdd: Product[] =
        prodArr === undefined ? [prod] : [...prodArr, prod]
      groupedProductsMap.set(prod.storeId, arrToAdd)
    } else groupedProductsMap.set(prod.storeId, [prod])
  })

  // create [Product[]]
  const groupedProducts = Array.from(groupedProductsMap.values())

  return groupedProducts
}

// ------------------------------------------------------------
//                        Server Communication

export async function serverSearchProducts(
  storeName: string | null,
  productName: string | null,
  category: string | null,
  keyword: string | null,
  productId: number | null,
  productIds: number[] | null
): Promise<ClientResponse<Map<number, Product[]>>> {
  const uri = serverPort + "/api/Buyers/SerachProducts"
  const jsonResponse = await fetch(uri, {
    method: "POST",
    headers: {
      accept: "text/plain",
      "Content-Type": "application/json",
    },
    // body: '{\n  "userId": 0,\n  "productId": 0,\n  "storeId": 0\n}',
    body: JSON.stringify({
      storeName: storeName,
      productName: productName,
      category: category,
      keyword: keyword,
      productId: productId,
      productIds: productIds,
    }),
  })

  const response = jsonResponse.json()
  return response
}
