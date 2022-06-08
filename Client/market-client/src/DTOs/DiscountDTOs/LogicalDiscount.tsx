import Expression from "./Expression";
import Discount from "./Discount";
import Store from "../Store";

class LogicalDiscount extends Discount {
    firstDiscount : Discount;
    secondDiscount : Discount;
    constructor(
        firstDiscount : Discount,
        secondDiscount : Discount) {
        super();
        this.firstDiscount = firstDiscount;
        this.secondDiscount = secondDiscount;
    }
    public toString = (store:Store) : string => {
        return `${this.firstDiscount} , ${this.secondDiscount}`;
    }
  }
  
  export default LogicalDiscount