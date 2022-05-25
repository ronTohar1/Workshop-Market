import Member from "../DTOs/Member"
import Store from '../DTOs/Store'
import { dummyStore1, dummyStore2 } from "./StoreService"

export const dummyMember1 = new Member(0,"username1",true)

export function getStoresManagedBy(member: Member) : Store[]{
    return [dummyStore1,dummyStore2]
}