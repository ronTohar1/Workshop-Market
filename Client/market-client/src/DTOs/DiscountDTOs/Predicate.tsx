import Store from "../Store";
import Expression from "./Expression";

class Predicate extends Expression {

    constructor(tag:string) {
        super(tag);
    }

    public toString = (store:Store) : string => {
        return `predicate`;
    }
  }
  
  export default Predicate