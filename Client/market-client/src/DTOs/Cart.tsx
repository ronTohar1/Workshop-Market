import Product from "./Product"
import ProductInBag from "./ProductInBag"
import ShoppingBag from "./ShoppingBag"
import Store from "./Store"

class Cart {
  shoppingBags: Map<number, ShoppingBag> // StoreId to Shopping Bag

  constructor(storeShoppingBags: Map<number, ShoppingBag>) {
    this.shoppingBags = storeShoppingBags
  }
}

export default Cart
