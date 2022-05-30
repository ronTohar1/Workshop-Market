import Product from "./Product"

class ShoppingBag {
  productsAmouns: Map<Product, number>
  storeId: number
  constructor(productsAmouns: Map<Product, number>, storeId: number) {
    this.storeId = storeId
    this.productsAmouns = productsAmouns
  }
}

export default ShoppingBag
