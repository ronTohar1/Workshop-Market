import Member from "../DTOs/Member";
import Purchase from "../DTOs/Purchase";
import { serverPort } from "../Utils";
import ClientResponse from "./Response";
export async function serverGetBuyerPurchaseHistory(
  buyerId: number,
  targetId: number
): Promise<ClientResponse<Purchase[]>> {
  const uri = serverPort + "/api/Admin/BuyerPurchaseHistory/2";
  const jsonResponse = await fetch(uri, {
    method: "POST",
    headers: {
      accept: "text/plain",
      "Content-Type": "application/json",
    },
    // body: '{\n  "userId": 0,\n  "targetId": 0\n}',
    body: JSON.stringify({
      userId: buyerId,
      targetId: targetId,
    }),
  });
  return jsonResponse.json();
}
export async function serverGetStorePurchaseHistory(
  userId: number,
  targetId: number
): Promise<ClientResponse<Purchase[]>> {
  const uri = serverPort + "/api/Admin/StorePurchaseHistory/0";
  const jsonResponse = await fetch(uri, {
    method: "POST",
    headers: {
      accept: "text/plain",
      "Content-Type": "application/json",
    },
    // body: '{\n  "userId": 0,\n  "targetId": 0\n}',
    body: JSON.stringify({
      userId: userId,
      targetId: targetId,
    }),
  });
  return jsonResponse.json();
}
export async function serverGetLoggedInMembers(
  userId: number
): Promise<ClientResponse<Member[]>> {
  const uri = serverPort + "/api/Admin/LoggedInUsers";
  const jsonResponse = await fetch(uri, {
    method: "POST",
    headers: {
      accept: "text/plain",
      "Content-Type": "application/json",
    },
    // body: '{\n  "userId": 0\n}',
    body: JSON.stringify({
      userId: userId,
    }),
  });
  return jsonResponse.json();
}

export async function serverGetAMemberInfo(
  userId: number,
  targetId: number
): Promise<ClientResponse<Member>> {
  const uri = serverPort + "/api/Admin/MemberInfo";
  const jsonResponse = await fetch(uri, {
    method: "POST",
    headers: {
      accept: "text/plain",
      "Content-Type": "application/json",
    },
    // body: '{\n  "userId": 0,\n  "targetId": 0\n}',
    body: JSON.stringify({
      userId: userId,
      targetId: targetId,
    }),
  });
  return jsonResponse.json();
}

export async function serverRemoveMember(
  userId: number,
  targetId: number
): Promise<ClientResponse<boolean>> {
  const uri = serverPort + "/api/Admin/RemoveMember/2";
  const jsonResponse = await fetch(uri, {
    method: "PUT",
    headers: {
      accept: "text/plain",
      "Content-Type": "application/json",
    },
    // body: '{\n  "userId": 0,\n  "targetId": 0\n}',
    body: JSON.stringify({
      userId: userId,
      targetId: targetId,
    }),
  });
  return jsonResponse.json();
}

export async function serverIsAdmin(
  adminId: number
): Promise<ClientResponse<boolean>> {
  const uri = serverPort + "/api/Admin/IsAdmin";
  const jsonResponse = await fetch(uri, {
    method: "POST",
    headers: {
      accept: "text/plain",
      "Content-Type": "application/json",
    },
    // body: '{\n  "userId": 0,\n  "targetId": 0\n}',
    body: JSON.stringify({
      userId: adminId,
    }),
  });
  console.log("request is admin");
  const response = jsonResponse.json();
  console.log("response");
  console.log(response);
  return response;
}

export async function serverGetDailyProfitOfAllStores(
  adminId: number
): Promise<ClientResponse<number>> {
  const uri = serverPort + "/api/Admin/GetSystemDailyProfit";
  const jsonResponse = await fetch(uri, {
    method: 'POST',
    headers: {
        'accept': 'text/plain',
        'Content-Type': 'application/json'
    },
    // body: '{\n  "memberId": 0\n}',
    body: JSON.stringify({
        'memberId': adminId
    })
});
  const response = jsonResponse.json();
  return response;
}
