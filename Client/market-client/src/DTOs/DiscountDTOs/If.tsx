import Conditional from "./Conditional";
import Discount from "./Discount";
import Expression from "./Expression";
import Predicate from "./Predicate";

class If extends Expression {
    test: Predicate;
    thenDis: Discount;
    elseDis: Discount;
    constructor(
        test: Predicate,
        thenDis: Discount,
        elseDis: Discount
    ) {
        super();
        this.test = test;
        this.thenDis = thenDis;
        this.elseDis = elseDis;
    }
  }
  
  export default If