
export default class ShoppingBag {
  productsAmounts: Map<number, number> // Map of product id --> quantity
  storeId: number
  constructor(productsAmounts: Map<number, number>, storeId: number) {
    this.storeId = storeId
    this.productsAmounts = productsAmounts
  }
}


