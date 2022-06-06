import Expression from "./Expression";
import Predicate from "./Predicate";

class Logical extends Predicate {
    firstExpression : Expression;
    secondExpression : Expression;
    constructor(
        firstExpression : Expression,
        secondExpression : Expression) {
        super();
        this.firstExpression = firstExpression;
        this.secondExpression = secondExpression;
    }
  }
  
  export default Logical