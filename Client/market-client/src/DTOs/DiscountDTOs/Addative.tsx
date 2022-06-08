import Store from "../Store";
import Conditional from "./Conditional";
import Discount from "./Discount";
import Expression from "./Expression";
import Predicate from "./Predicate";

class Addative extends Discount {
    discounts: Discount[];
    constructor(
        discounts: Discount[]
    ) {
        super();
        this.discounts = discounts;
    }
    public toString = (store:Store) : string => {
        return `THE DISCOUNT IS THE SUM OF THE NEXT DISCOUNTS:\n ${this.discounts.reduce((currStr, discount)=>currStr+discount.toString(store)+ ", ","")} `;
    }
  }
  
  export default Addative