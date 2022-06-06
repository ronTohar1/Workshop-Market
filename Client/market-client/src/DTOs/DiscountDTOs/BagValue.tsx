import Expression from "./Expression";
import Predicate from "./Predicate";

class BagValue extends Predicate {
    worth : number;
    constructor(
        worth : number,) {
        super();
        this.worth = worth;
    }
  }
  
  export default BagValue