import Store from "../Store";
import Discount from "./Discount";
import Predicate from "./Predicate";

class If extends Discount {
    test: Predicate;
    thenDis: Discount;
    elseDis: Discount | null;
    constructor(
        test: Predicate,
        thenDis: Discount,
        elseDis: Discount | null
    ) {
        super();
        this.test = test;
        this.thenDis = thenDis;
        this.elseDis = elseDis;
    }
    public toString = (store:Store) : string => {
        return `IF ${this.test.toString(store)} THEN: ${this.thenDis.toString(store)}${this.elseDis==null?"":`, OTHERWISE:${this.elseDis?.toString(store)}`}`;
    }
  }
  
  export default If