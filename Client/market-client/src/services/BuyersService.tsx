import Product from "../DTOs/Product";



const serverPort = 'https://localhost:7242';
export async function login(name: string,password: string):Promise<any> {
  const uri = serverPort+'/api/Buyers/Login';
  return await fetch(uri, {
    method: 'POST',
    headers: {
        'accept': 'text/plain',
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': 'http'
    },
    // body: '{\n  "userName": "string",\n  "password": "string"\n}',
    body: JSON.stringify({
        'userName': name,
        'password': password,
        'port': window.location.port
    })
}).then(response =>  alert(response.json()))
.then(data => {alert(data);})
.catch((error) => {alert(error.message);});};


export async function logout(userId: number):Promise<any> {
    const uri = serverPort+'/api/Buyers/Logout';
    return await fetch(uri, {
        method: 'POST',
    headers: {
        'accept': 'text/plain',
        'Content-Type': 'application/json'
    },
    // body: '{\n  "userId": userId\n}',
    body: JSON.stringify({
        'userId': userId
    })
    }).then(response =>  alert(response.json()))
  .then(data => {alert(data);})
  .catch((error) => {alert(error.message);});};


export async function register(name: string,password: string):Promise<any> {
    const uri = serverPort+'/api/Buyers/Register';
    return await fetch(uri, {
        method: 'POST',
        headers: {
            'accept': 'text/plain',
            'Content-Type': 'application/json'
        },
        // body: '{\n  "userName": "string",\n  "password": "string"\n}',
        body: JSON.stringify({
            'userName': name,
            'password': password
        })
    }).then(response =>  alert(response.json()))
  .then(data => {alert(data);})
  .catch((error) => {alert(error.message);});};

  export async function addToCart(userId: number,productId: number,storeId: number,amount: number):Promise<any> {
    const uri = serverPort+'/api/Buyers/AddProduct';
    return await fetch(uri, {
        method: 'POST',
        headers: {
            'accept': 'text/plain',
            'Content-Type': 'application/json'
        },
        // body: '{\n  "userId": 0,\n  "productId": 0,\n  "storeId": 0,\n  "amount": 0\n}',
        body: JSON.stringify({
            'userId': userId,
            'productId': productId,
            'storeId': storeId,
            'amount': amount
        })
    }).then(response =>  alert(response.json()))
  .then(data => {alert(data);})
  .catch((error) => {alert(error.message);});};


  export async function removeFromCart(userId: number,productId: number,storeId: number):Promise<any> {
    const uri = serverPort+'/api/Buyers/RemoveProduct';
    return await fetch(uri, {
        method: 'DELETE',
        headers: {
            'accept': 'text/plain',
            'Content-Type': 'application/json'
        },
        // body: '{\n  "userId": 0,\n  "productId": 0,\n  "storeId": 0\n}',
        body: JSON.stringify({
            'userId': userId,
            'productId': productId,
            'storeId': storeId
        })
    }).then(response =>  alert(response.json()))
  .then(data => {alert(data);})
  .catch((error) => {alert(error.message);});};


  export async function changeProductAmount(userId: number,productId: number,storeId: number,amount: number):Promise<any> {
    const uri = serverPort+'/api/Buyers/ChangeProductAmount';
    return await fetch(uri, {
        method: 'PUT',
    headers: {
        'accept': 'text/plain',
        'Content-Type': 'application/json'
    },
    // body: '{\n  "userId": 0,\n  "productId": 0,\n  "storeId": 0,\n  "amount": 0\n}',
    body: JSON.stringify({
        'userId': userId,
        'productId': productId,
        'storeId': storeId,
        'amount': amount
        })
    }).then(response =>  alert(response.json()))
  .then(data => {alert(data);})
  .catch((error) => {alert(error.message);});};


  export async function purchaseCart(userId: number):Promise<any> {
    const uri = serverPort+'/api/Buyers/PurchaseCart';
    return await fetch(uri, {
        method: 'POST',
        headers: {
            'accept': 'text/plain',
            'Content-Type': 'application/json'
        },
        // body: '{\n  "userId": 0\n}',
        body: JSON.stringify({
            'userId': userId
        })
    }).then(response =>  alert(response.json()))
  .then(data => {alert(data);})
  .catch((error) => {alert(error.message);});};


  export async function enterBuyerFacade():Promise<any> {
    const uri = serverPort+'/api/Buyers/PurchaseCart';
    return await fetch(uri, {
        headers: {
            'accept': 'text/plain'
        }
    }).then(response =>  alert(response.json()))
  .then(data => {alert(data);})
  .catch((error) => {alert(error.message);});};


  export async function leaveBuyerFacade(userId:number):Promise<any> {
    const uri = serverPort+'/api/Buyers/Leave';
    return await fetch(uri, {
        method: 'POST',
        headers: {
            'accept': 'text/plain',
            'Content-Type': 'application/json'
        },
        // body: '{\n  "userId": 0\n}',
        body: JSON.stringify({
            'userId': 0
        })
    }).then(response =>  alert(response.json()))
  .then(data => {alert(data);})
  .catch((error) => {alert(error.message);});};


  export async function storeInfo(storeId:number,storeName:string):Promise<any> {
    const uri = serverPort+'/api/Buyers/StoreInfo';
    return await fetch(uri, {
        method: 'POST',
        headers: {
            'accept': 'text/plain',
            'Content-Type': 'application/json'
        },
        // body: '{\n  "storeId": 0,\n  "storeName": "string"\n}',
        body: JSON.stringify({
            'storeId': storeId,
            'storeName': storeName
        })
    }).then(response =>  alert(response.json()))
  .then(data => {alert(data);})
  .catch((error) => {alert(error.message);});};


  export async function productsSearch(storeName:string,productName:string,category:string,keyword:string):Promise<any> {
    const uri = serverPort+'/api/Buyers/SerachProducts';
    return await fetch(uri, {
        method: 'POST',
        headers: {
            'accept': 'text/plain',
            'Content-Type': 'application/json'
        },
        // body: '{\n  "storeName": "string",\n  "productName": "string",\n  "category": "string",\n  "keyword": "string"\n}',
        body: JSON.stringify({
            'storeName': storeName,
            'productName': productName,
            'category': category,
            'keyword': keyword
        })
    }).then(response =>  alert(response.json()))
  .then(data => {alert(data);})
  .catch((error) => {alert(error.message);});};
 


  export async function reviewProduct(userId:number,storeId:number,productId:number,review:string):Promise<any> {
    const uri = serverPort+'/api/Buyers/ReviewProduct';
    return await fetch(uri, {
        method: 'POST',
        headers: {
            'accept': 'text/plain',
            'Content-Type': 'application/json'
        },
        // body: '{\n  "userId": 0,\n  "storeId": 0,\n  "productId": 0,\n  "review": "string"\n}',
        body: JSON.stringify({
            'userId': userId,
            'storeId': storeId,
            'productId': productId,
            'review': review
        })
    }).then(response =>  alert(response.json()))
  .then(data => {alert(data);})
  .catch((error) => {alert(error.message);});};
