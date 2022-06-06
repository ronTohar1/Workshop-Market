import Expression from "./Expression";
import Predicate from "./Predicate";
import StoreDiscount from "./StoreDiscount";

class ProductDiscount extends StoreDiscount {
    pid : number;
    constructor(
        pid : number,
        discount : number
        ) {
        super(discount);
        this.pid = pid;
    }
  }
  
  export default ProductDiscount