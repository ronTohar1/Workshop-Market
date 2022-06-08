export default class ProductInBag {
  productId: number
  storeId: number

  constructor(id: number, storeId: number) {
    this.productId = id
    this.storeId = storeId
  }
}
