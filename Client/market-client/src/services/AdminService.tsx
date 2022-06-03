
import Member from "../DTOs/Member";
import Purchase from "../DTOs/Purchase"; 
import { serverPort } from "../Utils";
import ClientResponse from "./Response";
export async function serverGetBuyerPurchaseHistory(
    buyerId: number,
    targetId: number,
  ): Promise<ClientResponse<Purchase[]>> {
    const uri = serverPort + "/api/Admin/BuyerPurchaseHistory/2";
    const jsonResponse = await fetch(uri, {
        method: 'POST',
        headers: {
            'accept': 'text/plain',
            'Content-Type': 'application/json'
        },
        // body: '{\n  "userId": 0,\n  "targetId": 0\n}',
        body: JSON.stringify({
            'userId': 0,
            'targetId': targetId
        })
    });
    return jsonResponse.json();
  }
  export async function serverGetStorePurchaseHistory(
    userId: number,
    targetId: number,
  ): Promise<ClientResponse<Purchase[]>> {
    const uri = serverPort + "/api/Admin/StorePurchaseHistory/0";
    const jsonResponse = await fetch(uri, {
        method: 'POST',
        headers: {
            'accept': 'text/plain',
            'Content-Type': 'application/json'
        },
        // body: '{\n  "userId": 0,\n  "targetId": 0\n}',
        body: JSON.stringify({
            'userId': 0,
            'targetId': targetId
        })
    });
    return jsonResponse.json();
  }
export async function serverGetLoggedInMembers(
    userId: number,
  ): Promise<ClientResponse<Member[]>> {
    const uri = serverPort + "/api/Admin/LoggedInUsers";
    const jsonResponse = await fetch(uri, {
        method: 'POST',
        headers: {
            'accept': 'text/plain',
            'Content-Type': 'application/json'
        },
        // body: '{\n  "userId": 0\n}',
        body: JSON.stringify({
            'userId': 0
        })
    });
    return jsonResponse.json();
  }

export async function serverGetAMemberInfo(
    userId: number,
    targetId: number,
  ): Promise<ClientResponse<Member>> {
    const uri = serverPort + "/api/Admin/MemberInfo";
    const jsonResponse = await fetch(uri, {
      method: 'POST',
      headers: {
          'accept': 'text/plain',
          'Content-Type': 'application/json'
      },
      // body: '{\n  "userId": 0,\n  "targetId": 0\n}',
      body: JSON.stringify({
          'userId': 0,
          'targetId': targetId
      })
    });
    return jsonResponse.json();
  }

  export async function removeMember(
    userId: number,
    targetId: number,
  ): Promise<ClientResponse<boolean>> {
    const uri = serverPort + "/api/Admin/RemoveMember/2";
    const jsonResponse = await fetch(uri, {
      method: 'PUT',
      headers: {
          'accept': 'text/plain',
          'Content-Type': 'application/json'
      },
      // body: '{\n  "userId": 0,\n  "targetId": 0\n}',
      body: JSON.stringify({
          'userId': 0,
          'targetId': targetId
      })
  });
    return jsonResponse.json();
  }