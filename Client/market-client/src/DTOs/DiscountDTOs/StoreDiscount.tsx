import Discount from "./Discount";
import Expression from "./Expression";
import Predicate from "./Predicate";

class StoreDiscount extends Discount {
    discount : number;
    constructor(
        discount : number,
        ) {
        super();
        this.discount = discount;
    }
  }
  
  export default StoreDiscount