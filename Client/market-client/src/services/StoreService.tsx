import Store from "../DTOs/Store";
import Product from "../DTOs/Product";
import { dummyProducts, groupByStore } from "../services/ProductsService";
import {serverPort} from "../services/BuyersService";

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

export async function addNewProduct(userId: number, product: Product):Promise<any> {
  const uri = serverPort+'/api/Stores/AddNewProduct';
  return await fetch(uri, {
    method: 'POST',
    headers: {
        'accept': 'text/plain',
        'Content-Type': 'application/json'
    },
    // body: '{\n  "userId": 0,\n  "storeId": 0,\n  "productName": "string",\n  "price": 0,\n  "category": "string"\n}',
    body: JSON.stringify({
        'userId': userId,
        'storeId': product.store,
        'productName': product.name,
        'price': product.price,
        'category': product.category
    })
});}


export async function addToProductInventory(userId: number, storeId:number, productId: number, amount:number):Promise<any> {
  const uri = serverPort+'/api/Stores/AddProduct';
  return await fetch(uri, {
    method: 'PUT',
    headers: {
        'accept': 'text/plain',
        'Content-Type': 'application/json'
    },
    // body: '{\n  "userId": 0,\n  "storeId": 0,\n  "productId": 0,\n  "amount": 0\n}',
    body: JSON.stringify({
      'userId': userId,
      'storeId':storeId,
      'productId': productId,
      'amount': amount
    })
});}


export async function removeFromProductInventory(userId: number, storeId:number, productId: number, amount:number):Promise<any> {
  const uri = serverPort+'/api/Stores/DecreaseProduct';
  return await fetch(uri, {
    method: 'PUT',
    headers: {
        'accept': 'text/plain',
        'Content-Type': 'application/json'
    },
    // body: '{\n  "userId": 0,\n  "storeId": 0,\n  "productId": 0,\n  "amount": 0\n}',
    body: JSON.stringify({
       'userId': userId,
        'storeId':storeId,
        'productId': productId,
        'amount': amount
    })
});}


export async function makeCoOwner(userId: number, storeId: number, targetUserId:number):Promise<any> {
  const uri = serverPort+'/api/Stores/MakeCoOwner';
  return await fetch(uri, {
    method: 'PUT',
    headers: {
        'accept': 'text/plain',
        'Content-Type': 'application/json'
    },
    // body: '{\n  "userId": 0,\n  "storeId": 0,\n  "targetUserId": 0\n}',
    body: JSON.stringify({
        'userId': userId,
        'storeId': storeId,
        'targetUserId': targetUserId
    })
});}


export async function removeCoOwner(userId: number, storeId: number, targetUserId:number):Promise<any> {
  const uri = serverPort+'/api/Stores/RemoveCoOwner';
  return await fetch(uri, {
    method: 'PUT',
    headers: {
        'accept': 'text/plain',
        'Content-Type': 'application/json'
    },
    // body: '{\n  "userId": 0,\n  "storeId": 0,\n  "targetUserId": 0\n}',
    body: JSON.stringify({
      'userId': userId,
      'storeId': storeId,
      'targetUserId': targetUserId
    })
});}


export async function makeCoManager(userId: number, storeId: number, targetUserId:number):Promise<any> {
  const uri = serverPort+'/api/Stores/MakeCoManager';
  return await fetch(uri, {
    method: 'PUT',
    headers: {
        'accept': 'text/plain',
        'Content-Type': 'application/json'
    },
    // body: '{\n  "userId": 0,\n  "storeId": 0,\n  "targetUserId": 0\n}',
    body: JSON.stringify({
      'userId': userId,
      'storeId': storeId,
      'targetUserId': targetUserId
    })
});}


export async function getMembersInRoles(userId: number, storeId: number, role:number):Promise<any> {
  const uri = serverPort+'/api/Stores/MembersInRole';
  return await fetch(uri, {
    method: 'POST',
    headers: {
        'accept': 'text/plain',
        'Content-Type': 'application/json'
    },
    // body: '{\n  "userId": 0,\n  "storeId": 0,\n  "role": 0\n}',
    body: JSON.stringify({
        'userId': userId,
        'storeId': storeId,
        'role': role
    })
});}

export async function getFounder(userId: number, storeId: number):Promise<any> {
  const uri = serverPort+'/api/Stores/Founder';
  return await fetch(uri, {
    method: 'POST',
    headers: {
        'accept': 'text/plain',
        'Content-Type': 'application/json'
    },
    // body: '{\n  "userId": 0,\n  "storeId": 0\n}',
    body: JSON.stringify({
        'userId': userId,
        'storeId': storeId
    })
});}


export async function getManagerPermission(userId: number, storeId: number, targetUserId: number):Promise<any> {
  const uri = serverPort+'/api/Stores/ManagerPermissions';
  return await fetch(uri, {
    method: 'POST',
    headers: {
        'accept': 'text/plain',
        'Content-Type': 'application/json'
    },
    // body: '{\n  "userId": 0,\n  "storeId": 0,\n  "targetUserId": 0\n}',
    body: JSON.stringify({
        'userId': userId,
        'storeId': storeId,
        'targetUserId': targetUserId
    })
});}


export async function setManagerPermission(userId: number, storeId: number, targetUserId: number, newPermissions: number[]):Promise<any> {
  const uri = serverPort+'/api/Stores/ChangeManagerPermission';
  return await fetch(uri, {
    method: 'POST',
    headers: {
        'accept': 'text/plain',
        'Content-Type': 'application/json'
    },
    // body: '{\n  "userId": 0,\n  "storeId": 0,\n  "targetUserId": 0,\n  "permissions": [\n    0\n  ]\n}',
    body: JSON.stringify({
        'userId': userId,
        'storeId': storeId,
        'targetUserId': targetUserId,
        'permissions': newPermissions
    })
});}


export async function getPurchaseHistory(userId: number, storeId: number):Promise<any> {
  const uri = serverPort+'/api/Stores/PurchaseHistory';
  return await fetch(uri, {
    method: 'POST',
    headers: {
        'accept': 'text/plain',
        'Content-Type': 'application/json'
    },
    // body: '{\n  "userId": 0,\n  "storeId": 0\n}',
    body: JSON.stringify({
        'userId': userId,
        'storeId': storeId
    })
});}


export async function openStore(userId: number, storeName: string):Promise<any> {
  const uri = serverPort+'/api/Stores/OpenStore';
  try{
      const fetched =  fetch(uri, {
        method: 'POST',
        headers: {
            'accept': 'text/plain',
            'Content-Type': 'application/json'
        },
        // body: '{\n  "userId": 0,\n  "storeName": "string"\n}',
        body: JSON.stringify({
            'userId': userId,
            'storeName': storeName
        })
    });
    return fetched;
  }
  catch(error){
    
  }
}

//.then(response=>Promise.resolve(response.json().then((data)=>data)))
export async function closeStore(userId: number, storeId: number):Promise<any> {
  const uri = serverPort+'/api/Stores/CloseStore';
  return await fetch(uri, {
    method: 'DELETE',
    headers: {
        'accept': 'text/plain',
        'Content-Type': 'application/json'
    },
    // body: '{\n  "userId": 0,\n  "storeId": 0\n}',
    body: JSON.stringify({
        'userId': userId,
        'storeId': storeId
    })
});}
//////////////////////////////// ---  ADD FREAKING DISCOUNTS AND PURCHASES ---- ///////

// export async function addDiscountPolicy(userId: number, storeId: number,,description:string):Promise<any> {
//   const uri = serverPort+'/api/Stores/AddDiscountPolicy';
//   return await fetch(uri, {
//     method: 'POST',
//     headers: {
//         'accept': 'text/plain',
//         'Content-Type': 'application/json'
//     },
//     // body: '{\n  "userId": 0,\n  "storeId": 0,\n  "expression": {},\n  "description": "string"\n}',
//     body: JSON.stringify({
//         'userId': userId,
//         'storeId': storeId,
//         'expression': {},
//         'description': description
//     })
// });}

