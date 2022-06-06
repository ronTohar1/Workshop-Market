import Conditional from "./Conditional";
import Discount from "./Discount";
import Expression from "./Expression";
import Predicate from "./Predicate";

class Max extends Discount {
    discounts: Discount[];
    constructor(
        discounts: Discount[]
    ) {
        super();
        this.discounts = discounts;
    }
  }
  
  export default Max