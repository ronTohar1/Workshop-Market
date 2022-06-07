import Store from "../Store";
import Conditional from "./Conditional";
import Discount from "./Discount";
import Expression from "./Expression";
import Predicate from "./Predicate";

class Max extends Discount {
    discounts: Discount[];
    constructor(
        discounts: Discount[]
    ) {
        super();
        this.discounts = discounts;
    }
    public toString = (store:Store) : string => {
        return `the discount for the basket will be the one with the maximal value from the discounts:\n ${this.discounts.reduce((currStr, discount)=>currStr+", \n"+discount.toString(store),"")} `;
    }
  }
  
  export default Max