import Store from "../DTOs/Store";
import Product from "../DTOs/Product";
import { dummyProducts, groupByStore } from "../services/ProductsService";

const stores = [
  new Store(0, "Ronto's", groupByStore(dummyProducts)[0]),
  new Store(1, "Mithcell's", groupByStore(dummyProducts)[1]),
];
export const dummyStore1 = stores[0];
export const dummyStore2 = stores[1];

export const getStore: (id: number) => Store = (id: number) => {
  const store = stores.find((store) => store.id === id);
  if (store === undefined) throw new Error("Store doesnt exist with id " + id);
  return store;
};

export function groupStoresProducts(stores: Store[]): Product[][] {
  return stores.map((store: Store) => store.products);
}

export async function addNewProduct(product: Product) {
  dummyStore1.products = [...dummyStore1.products, product]; //TODO: change to the real implementation
  dummyStore2.products = [...dummyStore2.products, product]; //TODO: change to the real implementation
  // await post('http://localhost:5000/stores?id=123', toJson(product))
}
