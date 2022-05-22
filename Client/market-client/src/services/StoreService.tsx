
import { Store } from '../DTOs/Store';
import  { dummyProducts } from "../services/ProductsService";

const stores = [
                new Store(0,"Ronto's", dummyProducts.slice(0,3)),
                new Store(1,"Mithcell's", dummyProducts.slice(3))
                ];
export const dummyStore1 = stores[0]
export const dummyStore2 = stores[1]

export const getStore : (id:number) => Store = (id: number) => {
    const store =  stores.find((store) => store.id === id)
    if (store === undefined)
        throw new Error("Store doesnt exist with id "+ id );
    return store
}   
