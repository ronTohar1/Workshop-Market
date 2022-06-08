import Store from "../Store";
import Expression from "./Expression";
import Predicate from "./Predicate";

class BagValue extends Predicate {
    worth : number;
    constructor(
        worth : number,) {
        super();
        this.worth = worth;
    }
    public toString = (store:Store) : string => {
        return `THE BAG COST IS AT LEAST ${this.worth} ₪`;
    }
  }
  
  export default BagValue