import Product from "../DTOs/Product";
import { IDictionary, serverPort } from "../Utils";
import ClientResponse from "./Response";

export const dummyProducts = [
  new Product(1, "apple", 2.4, "fruits", 0, 100),
  new Product(2, "keyboard", 199, "elecronics", 1, 100),
  new Product(3, "banana", 3.5, "fruits", 0, 100),
  new Product(4, "pineapple", 9, "fruits", 0, 100),
  new Product(5, "xiaomi 9", 250, "cellphones", 1, 100),
  new Product(6, "xiaomi 10", 300, "cellphones", 1, 100),
  new Product(7, "milk", 1.5, "dairy", 0, 100),
  new Product(8, "butter", 1.8, "dairy", 0, 100),
  new Product(9, "cheese", 1.8, "dairy", 0, 100),
];
// Fetch Products
// should be async
export const fetchProducts = async (query: string): Promise<Product[][]> => {
  try {
    const promiseResponse = serverSearchProducts(null, query, null, null);

    const serverResponse = await promiseResponse;
    
    const productsByStore : any = serverResponse.value; // Dicitionary of store to product
    if (serverResponse.errorOccured) {
      alert("Whoops! " + serverResponse.errorMessage);
      return [];
    }

    const allProducts : Product[][] = []

    for (const storeId in productsByStore) {
      allProducts.push(productsByStore[Number(storeId)])
    }
    return allProducts;
    
  } catch (e) {
    return [];
  }
};

// const takeMapValues = (prodsMap: Map<number,Product[]>) : Product[][] => {
//   if(prodsMap.)

//   return [[]]

// }

export function groupByStore(products: Product[]): Product[][] {
  const groupedProductsMap: Map<number, Product[]> = new Map();
  products.forEach((prod: Product) => {
    if (groupedProductsMap.has(prod.store)) {
      const prodArr = groupedProductsMap.get(prod.store);
      const arrToAdd: Product[] =
        prodArr === undefined ? [prod] : [...prodArr, prod];
      groupedProductsMap.set(prod.store, arrToAdd);
    } else groupedProductsMap.set(prod.store, [prod]);
  });

  // create [Product[]]
  const groupedProducts = Array.from(groupedProductsMap.values());

  return groupedProducts;
}

// ------------------------------------------------------------
//                        Server Communication

export async function serverSearchProducts(
  storeName: string | null,
  productName: string | null,
  category: string | null,
  keyword: string | null
): Promise<ClientResponse<Map<number, Product[]>>> {
  const uri = serverPort + "/api/Buyers/SerachProducts";
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
    }),
  });

  const response = jsonResponse.json();
  return response;
}
