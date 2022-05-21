
import { Store } from '../DTOs/Store';
import  { dummyProducts } from "../services/ProductsService";

export const dummyStore1 = new Store(0,"cool Store", dummyProducts.slice(0,3))
export const dummyStore2 = new Store(0,"cool Store", dummyProducts.slice(3))