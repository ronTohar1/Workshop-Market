import Store from "../Store";
import Discount from "./Discount";
import Expression from "./Expression";
import Predicate from "./Predicate";

class StoreDiscount extends Discount {
    discount : number;
    constructor(
        discount : number,
        ) {
        super();
        this.discount = discount;
    }
    public toString = (store:Store) : string => {
        return `The store owner lost his mind! there is a ${this.discount}% discount on all the store!`;
    }
  }
  
  export default StoreDiscount