import Store from "../Store";
import Expression from "./Expression";
import Logical from "./Logical";
import Predicate from "./Predicate";

class And extends Logical {

    constructor(
        firstExpression : Predicate,
        secondExpression : Predicate) {
        super(firstExpression, secondExpression,'AndPredicate');
    }
    public toString = (store:Store) : string => {
        return `${this.firstExpression.toString(store)} AND ${this.secondExpression.toString(store)}`;
    }
  }
  
  export default And