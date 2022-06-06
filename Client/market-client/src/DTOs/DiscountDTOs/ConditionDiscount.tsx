import Conditional from "./Conditional";
import Discount from "./Discount";
import Expression from "./Expression";
import Predicate from "./Predicate";

class ConditionDiscount extends Conditional {
    pred: Predicate;
    then: Discount;
    constructor(
        pred: Predicate,
        then: Discount
    ) {
        super();
        this.pred = pred;
        this.then = then;
    }
  }
  
  export default ConditionDiscount