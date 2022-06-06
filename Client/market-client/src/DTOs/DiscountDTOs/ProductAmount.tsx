import Expression from "./Expression";
import Predicate from "./Predicate";

class ProductAmount extends Predicate {
    pid : number;
    quantity : number;
    constructor(
        pid : number,
        quantity : number
        ) {
        super();
        this.pid = pid;
        this.quantity = quantity;
    }
  }
  
  export default ProductAmount