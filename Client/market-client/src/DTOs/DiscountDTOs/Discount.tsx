import Store from "../Store";
import Expression from "./Expression";

class Discount extends Expression {

    constructor() {
        super();
    }
    public toString = (store:Store) : string => {
        return `discount`;
    }
  }
  
  export default Discount