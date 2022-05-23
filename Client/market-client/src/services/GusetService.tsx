import Product from "../DTOs/Product";


const uri = 'https://localhost:7242/api/Buyers/Login';
// Fetch Products
// should be async
export async function login(name: string,password: string):Promise<any> {
  // const res = await fetch('http://localhost:5000/products/query=...')
  // const data = await res.json()
  return await fetch(uri, {
    method: 'POST',
    headers: {
        'accept': 'text/plain',
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': 'http'
    },
    // body: '{\n  "userName": "string",\n  "password": "string"\n}',
    body: JSON.stringify({
        'userName': 'string',
        'password': 'string'
    })
}).then(response =>  alert(response.json()))
.then(data => {
    alert(data);
})
.catch((error) => {
    alert(error.message);
})
;
};
