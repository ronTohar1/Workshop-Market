import Store from "../Store";
import Logical from "./Logical";
import Predicate from "./Predicate";

class Or extends Logical {

    constructor(
        firstExpression : Predicate,
        secondExpression : Predicate) {
        super(firstExpression, secondExpression,'OrPredicate');
    }
    public toString = (store:Store) : string => {
        return `${this.firstExpression.toString(store)} OR ${this.secondExpression.toString(store)}`;
    }
  }
  
  export default Or