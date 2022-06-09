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
        super("maxDiscount");
        this.discounts = discounts;
    }
    public toString = (store:Store) : string => {
        return `THE DISCOUNT IS THE MAXIMUM DISCOUNT FROM THE NEXT DISCOUNTS:\n ${this.discounts.reduce((currStr, discount)=>currStr+discount.toString(store)+ ", ","")} `;
    }
  }
  
  export default Max