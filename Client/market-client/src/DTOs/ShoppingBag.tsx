import Product from "./Product";
class ShoppingBag {
    productsAmouns: Map<Product, number>
  
    constructor(productsAmouns:  Map<Product, number>,) {
      this.productsAmouns = productsAmouns;
    }
  }
  
  export default ShoppingBag;
  