import Product from "../DTOs/Product";
import { serverPort } from "../Utils";
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
export const fetchProducts = (query: string) => {
  // const res = await fetch('http://localhost:5000/products/query=...')
  // const data = await res.json()

  return dummyProducts.filter((p) => p.name.includes(query));
};

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
  storeName: string,
  productName: string,
  category: string,
  keyword: string
): Promise<ClientResponse<Map<number,Product[]>>> {
  const uri = serverPort + "/api/Buyers/RemoveProduct";
  const response = await fetch(uri, {
    method: "DELETE",
    headers: {
      accept: "text/plain",
      "Content-Type": "application/json",
    },
    // body: '{\n  "userId": 0,\n  "productId": 0,\n  "storeId": 0\n}',
    body: JSON.stringify({
      storeName: storeName,
      productName: productName,
      category: category,
      keyword: keyword
    }),
  });

  
}