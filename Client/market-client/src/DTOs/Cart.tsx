import Product from "./Product"
import ProductInBag from "./ProductInBag"
import ShoppingBag from "./ShoppingBag"
import Store from "./Store"

class Cart {
  storeShoppingBags: Map<ProductInBag, ShoppingBag> // StoreId to Shopping Bag

  constructor(storeShoppingBags: Map<ProductInBag, ShoppingBag>) {
    this.storeShoppingBags = storeShoppingBags
  }
}

export default Cart
