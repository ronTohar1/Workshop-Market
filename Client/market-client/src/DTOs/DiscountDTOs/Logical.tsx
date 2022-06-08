import Expression from "./Expression";
import Predicate from "./Predicate";

class Logical extends Predicate {
    firstExpression : Predicate;
    secondExpression : Predicate;
    constructor(
        firstExpression : Predicate,
        secondExpression : Predicate) {
        super();
        this.firstExpression = firstExpression;
        this.secondExpression = secondExpression;
    }
  }
  
  export default Logical