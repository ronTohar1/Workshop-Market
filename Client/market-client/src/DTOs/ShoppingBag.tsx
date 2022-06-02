import Product from "./Product"

class ShoppingBag {
  productsAmouns: Map<number, number> // Map of product id --> quantity
  storeId: number
  constructor(productsAmouns: Map<number, number>, storeId: number) {
    this.storeId = storeId
    this.productsAmouns = productsAmouns
  }
}

export default ShoppingBag
