import Predicate from "./Predicate";

class CheckProductLess extends Predicate {
    productId: number;
    amount: number;
    constructor(
        productId: number,
        amount: number) {
        super();
        this.productId = productId;
        this.amount = amount;
    }
  }
  
  export default CheckProductLess