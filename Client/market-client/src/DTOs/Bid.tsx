interface Bid {
  id: number
  storeId: number
  productId: number
  memberId: number
  bid: number
  approvingIds: number[]
  counterOffer: boolean
  offer: number
}

export default Bid
