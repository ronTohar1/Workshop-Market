import Cart from "../DTOs/Cart"
import CheckoutDTO from "../DTOs/CheckoutDTO"
import Product from "../DTOs/Product"
import Purchase from "../DTOs/Purchase"
import { serverPort } from "../Utils"
import ClientResponse from "./Response"

export async function serverEnter(): Promise<ClientResponse<number>> {
  const uri = serverPort + "/api/Buyers/Enter"
  const jsonResponse = await fetch(uri)

  return jsonResponse.json()
}

export async function serverLogin(
  name: string | undefined | null,
  password: string | undefined | null
): Promise<ClientResponse<number>> {
  if (name == undefined || password == undefined) return Promise.reject()
  const uri = serverPort + "/api/Buyers/Login"
  try {
    const jsonResponse = await fetch(uri, {
      method: "POST",
      headers: {
        accept: "text/plain",
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "http",
      },
      // body: '{\n  "userName": "string",\n  "password": "string"\n}',
      body: JSON.stringify({
        userName: name,
        password: password,
        port: window.location.port,
      }),
    })

    const response = jsonResponse.json()
    return response
  } catch (e) {
    return Promise.reject(e)
  }
}

//.then(response=>Promise.resolve(response.json().then((data)=>data)))

export async function logout(userId: number): Promise<any> {
  const uri = serverPort + "/api/Buyers/Logout"
  return await fetch(uri, {
    method: "POST",
    headers: {
      accept: "text/plain",
      "Content-Type": "application/json",
    },
    // body: '{\n  "userId": userId\n}',
    body: JSON.stringify({
      userId: userId,
    }),
  })
}

export async function serverRegister(
  name: string | undefined,
  password: string | undefined
): Promise<ClientResponse<number>> {
  const uri = serverPort + "/api/Buyers/Register"
  const jsonResponse = await fetch(uri, {
    method: "POST",
    headers: {
      accept: "text/plain",
      "Content-Type": "application/json",
    },
    // body: '{\n  "userName": "string",\n  "password": "string"\n}',
    body: JSON.stringify({
      userName: name,
      password: password,
    }),
  })

  const response = jsonResponse.json()
  console.log(response)
  return response
}

export async function serverAddToCart(
  userId: number,
  productId: number,
  storeId: number,
  amount: number
): Promise<ClientResponse<boolean>> {
  const uri = serverPort + "/api/Buyers/AddProduct"
  const jsonResponse = await fetch(uri, {
    method: "POST",
    headers: {
      accept: "text/plain",
      "Content-Type": "application/json",
    },
    // body: '{\n  "userId": 0,\n  "productId": 0,\n  "storeId": 0,\n  "amount": 0\n}',
    body: JSON.stringify({
      userId: userId,
      productId: productId,
      storeId: storeId,
      amount: amount,
    }),
  })
  return jsonResponse.json()
}

export async function serverRemoveFromCart(
  userId: number,
  productId: number,
  storeId: number
): Promise<ClientResponse<boolean>> {
  const uri = serverPort + "/api/Buyers/RemoveProduct"
  const jsonResponse = await fetch(uri, {
    method: "DELETE",
    headers: {
      accept: "text/plain",
      "Content-Type": "application/json",
    },
    // body: '{\n  "userId": 0,\n  "productId": 0,\n  "storeId": 0\n}',
    body: JSON.stringify({
      userId: userId,
      productId: productId,
      storeId: storeId,
    }),
  })

  return jsonResponse.json()
}

export async function serverChangeProductAmount(
  userId: number,
  productId: number,
  storeId: number,
  amount: number
): Promise<ClientResponse<boolean>> {
  const uri = serverPort + "/api/Buyers/ChangeProductAmount"
  const jsonResponse = await fetch(uri, {
    method: "PUT",
    headers: {
      accept: "text/plain",
      "Content-Type": "application/json",
    },
    // body: '{\n  "userId": 0,\n  "productId": 0,\n  "storeId": 0,\n  "amount": 0\n}',
    body: JSON.stringify({
      userId: userId,
      productId: productId,
      storeId: storeId,
      amount: amount,
    }),
  })
  return jsonResponse.json()
}

export async function purchaseCart(
  userId: number,
  checkout: CheckoutDTO
): Promise<ClientResponse<Purchase>> {
  const uri = serverPort + "/api/Buyers/PurchaseCart"
  const jsonResponse = await fetch(uri, {
    method: "POST",
    headers: {
      accept: "text/plain",
      "Content-Type": "application/json",
    },
    // body: '{\n  "userId": 0,\n  "cardNumber": "string",\n  "month": "string",\n  "year": "string",\n  "holder": "string",\n  "ccv": "string",\n  "id": "string",\n  "receiverName": "string",\n  "address": "string",\n  "city": "string",\n  "country": "string",\n  "zip": "string"\n}',
    body: JSON.stringify({
      userId: userId,
      cardNumber: checkout.cardNumber,
      month: checkout.month,
      year: checkout.year,
      holder: checkout.nameOnCard,
      ccv: checkout.ccv,
      id: checkout.id,
      receiverName: checkout.firstName + " " + checkout.lastName,
      address: checkout.address,
      city: checkout.city,
      country: checkout.country,
      zip: checkout.zip,
    }),
  })
  return jsonResponse.json()
}

export async function serverGetCart(
  userId: number
): Promise<ClientResponse<Cart>> {
  const uri = serverPort + "/api/Buyers/GetBuyerCart"

  const jsonResponse = await fetch(uri, {
    method: "POST",
    headers: {
      accept: "text/plain",
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      userId: userId,
    }),
  })
  const response = jsonResponse.json()
  return response
}

export async function enterBuyerFacade(): Promise<any> {
  const uri = serverPort + "/api/Buyers/PurchaseCart"
  return await fetch(uri, {
    headers: {
      accept: "text/plain",
    },
  })
}

export async function leaveBuyerFacade(userId: number): Promise<any> {
  const uri = serverPort + "/api/Buyers/Leave"
  return await fetch(uri, {
    method: "POST",
    headers: {
      accept: "text/plain",
      "Content-Type": "application/json",
    },
    // body: '{\n  "userId": 0\n}',
    body: JSON.stringify({
      userId: 0,
    }),
  })
}

export async function storeInfo(
  storeId: number,
  storeName: string
): Promise<any> {
  const uri = serverPort + "/api/Buyers/StoreInfo"
  return await fetch(uri, {
    method: "POST",
    headers: {
      accept: "text/plain",
      "Content-Type": "application/json",
    },
    // body: '{\n  "storeId": 0,\n  "storeName": "string"\n}',
    body: JSON.stringify({
      storeId: storeId,
      storeName: storeName,
    }),
  })
}

export async function productsSearch(
  storeName: string,
  productName: string,
  category: string,
  keyword: string
): Promise<any> {
  const uri = serverPort + "/api/Buyers/SerachProducts"
  return await fetch(uri, {
    method: "POST",
    headers: {
      accept: "text/plain",
      "Content-Type": "application/json",
    },
    // body: '{\n  "storeName": "string",\n  "productName": "string",\n  "category": "string",\n  "keyword": "string"\n}',
    body: JSON.stringify({
      storeName: storeName,
      productName: productName,
      category: category,
      keyword: keyword,
    }),
  })
}

export async function reviewProduct(
  userId: number,
  storeId: number,
  productId: number,
  review: string
): Promise<any> {
  const uri = serverPort + "/api/Buyers/ReviewProduct"
  return await fetch(uri, {
    method: "POST",
    headers: {
      accept: "text/plain",
      "Content-Type": "application/json",
    },
    // body: '{\n  "userId": 0,\n  "storeId": 0,\n  "productId": 0,\n  "review": "string"\n}',
    body: JSON.stringify({
      userId: userId,
      storeId: storeId,
      productId: productId,
      review: review,
    }),
  })
}
