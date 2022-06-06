import Expression from "./Expression";
import Logical from "./Logical";

class Xor extends Logical {

    constructor(
        firstExpression : Expression,
        secondExpression : Expression) {
        super(firstExpression, secondExpression);
    }
  }
  
  export default Xor