import Store from "../DTOs/Store";
import Member from "../DTOs/Member";
import Product from "../DTOs/Product";
import { dummyProducts, groupByStore } from "../services/ProductsService";
import {serverPort} from '../Utils'
import ClientResponse from "../services/Response";
import List from "@mui/material/List";
import Purchase from "../DTOs/Purchase";
const stores = [
  new Store(0, "Ronto's", groupByStore(dummyProducts)[0]),
  new Store(1, "Mithcell's", groupByStore(dummyProducts)[1]),
];
export const dummyStore1 = stores[0];
export const dummyStore2 = stores[1];

export const getStore = (id: number) => {
  const store = stores.find((store) => store.id === id);
  if (store === undefined) throw new Error("Store not found");
  return store;
};

export function groupStoresProducts(stores: Store[]): Product[][] {
  return stores.map((store: Store) => store.products);
}
// export async function serverLogin(
//   name: string | undefined | null,
//   password: string | undefined| null
// ): Promise<Response<number>> {
//   if (name == undefined || password == undefined) return Promise.reject();
//   const uri = serverPort + "/api/Buyers/Login";
//   const jsonResponse = await fetch(uri, {
//     method: "POST",
//     headers: {
//       accept: "text/plain",
//       "Content-Type": "application/json",
//       "Access-Control-Allow-Origin": "http",
//     },
//     // body: '{\n  "userName": "string",\n  "password": "string"\n}',
//     body: JSON.stringify({
//       userName: name,
//       password: password,
//       port: window.location.port,
//     }),
//   });

//   return jsonResponse.json();
// }
export async function addNewProduct(userId: number, product: Product): Promise<ClientResponse<number>> {
  const uri = serverPort+'/api/Stores/AddNewProduct';
  const jsonResponse = await fetch(uri, {
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
});
  return  jsonResponse.json();
}


export async function addToProductInventory(userId: number, storeId:number, productId: number, amount:number):Promise<ClientResponse<boolean>> {
  const uri = serverPort+'/api/Stores/AddProduct';
  const jsonResponse = await fetch(uri, {
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
});
  return jsonResponse.json();
}


export async function removeFromProductInventory(userId: number, storeId:number, productId: number, amount:number):Promise<ClientResponse<boolean>> {
  const uri = serverPort+'/api/Stores/DecreaseProduct';
  const jsonResponse = await fetch(uri, {
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
});
  return jsonResponse.json();
}


export async function makeCoOwner(userId: number, storeId: number, targetUserId:number):Promise<ClientResponse<boolean>> {
  const uri = serverPort+'/api/Stores/MakeCoOwner';
  const jsonResponse = await fetch(uri, {
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
});
  return jsonResponse.json();
}


export async function removeCoOwner(userId: number, storeId: number, targetUserId:number):Promise<ClientResponse<boolean>> {
  const uri = serverPort+'/api/Stores/RemoveCoOwner';
  const jsonResponse =  await fetch(uri, {
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
});
  return jsonResponse.json()
}


export async function makeCoManager(userId: number, storeId: number, targetUserId:number):Promise<ClientResponse<boolean>> {
  const uri = serverPort+'/api/Stores/MakeCoManager';
  const jsonResponse =  await fetch(uri, {
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
});
  return jsonResponse.json();
}


export async function getMembersInRoles(userId: number, storeId: number, role:number):Promise<ClientResponse<boolean>> {
  const uri = serverPort+'/api/Stores/MembersInRole';
  const jsonResponse =  await fetch(uri, {
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
});
  return jsonResponse.json();
}

export async function getFounder(userId: number, storeId: number):Promise<ClientResponse<Member>> {
  const uri = serverPort+'/api/Stores/Founder';
  const jsonResponse = await fetch(uri, {
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
});
return jsonResponse.json();
}


export async function getManagerPermission(userId: number, storeId: number, targetUserId: number):Promise<ClientResponse<number[]>> {
  const uri = serverPort+'/api/Stores/ManagerPermissions';
  const jsonResponse = await fetch(uri, {
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
});
  return jsonResponse.json();
}


export async function setManagerPermission(userId: number, storeId: number, targetUserId: number, newPermissions: number[]):Promise<ClientResponse<boolean>> {
  const uri = serverPort+'/api/Stores/ChangeManagerPermission';
  const jsonResponse = await fetch(uri, {
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
});
    return jsonResponse.json();
}


export async function getPurchaseHistory(userId: number, storeId: number):Promise<ClientResponse<Purchase[]>> {
  const uri = serverPort+'/api/Stores/PurchaseHistory';
  const jsonResponse = await fetch(uri, {
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
});
  return jsonResponse.json();
}


export async function openStore(userId: number, storeName: string):Promise<ClientResponse<number>> {
  const uri = serverPort+'/api/Stores/OpenStore';
    const jsonResponse = await fetch(uri, {
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
    }); return jsonResponse.json();
}

//.then(response=>Promise.resolve(response.json().then((data)=>data)))
export async function closeStore(userId: number, storeId: number):Promise<ClientResponse<boolean>> {
  const uri = serverPort+'/api/Stores/CloseStore';
  const jsonResponse = await fetch(uri, {
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
});return jsonResponse.json();
}
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

