import Store from "../Store";
import Expression from "./Expression";

class Discount extends Expression {

    constructor(tag: string) {
        super(tag);
    }
    public toString = (store:Store) : string => {
        return `discount`;
    }
  }
  
  export default Discount