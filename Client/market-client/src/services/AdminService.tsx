
import Purchase from "../DTOs/Purchase"; 
import { serverPort } from "../Utils";
import ClientResponse from "./Response";
export async function serverGetBuyerPurchaseHistory(
    buyerId: number,
    targetId: number,
  ): Promise<ClientResponse<Purchase[]>> {
    const uri = serverPort + "/api/Stores/AddNewProduct";
    const jsonResponse = await fetch(uri, {
        method: 'POST',
        headers: {
            'accept': 'text/plain',
            'Content-Type': 'application/json'
        },
        // body: '{\n  "userId": 0,\n  "targetId": 0\n}',
        body: JSON.stringify({
            'userId': buyerId,
            'targetId': targetId
        })
    });
    return jsonResponse.json();
  }