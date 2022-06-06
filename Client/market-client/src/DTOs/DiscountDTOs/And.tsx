import Expression from "./Expression";
import Logical from "./Logical";
import Predicate from "./Predicate";

class And extends Logical {

    constructor(
        firstExpression : Expression,
        secondExpression : Expression) {
        super(firstExpression, secondExpression);
    }
  }
  
  export default And