import Store from "../Store";
import Logical from "./Logical";
import Predicate from "./Predicate";

class Xor extends Logical {

    constructor(
        firstExpression : Predicate,
        secondExpression : Predicate) {
        super(firstExpression, secondExpression,'XorPredicate');
    }
    public toString = (store:Store) : string => {
        return `${this.firstExpression.toString(store)} OR ${this.secondExpression.toString(store)} BUT NOT BOTH!`;
    }
  }
  
  export default Xor