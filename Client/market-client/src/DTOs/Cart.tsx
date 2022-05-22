
import ShoppingBag from "./ShoppingBag";
import Store from "./Store";

class Cart {
    storeShoppingBags: Map<Store, ShoppingBag>
    
    constructor(storeShoppingBags:  Map<Store, ShoppingBag>) {
      this.storeShoppingBags = storeShoppingBags;
    }
  }
  
  export default Cart;
  