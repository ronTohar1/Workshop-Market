import LogicalDiscount from "./LogicalDiscount";
import Discount from "./Discount";
import Store from "../Store";

class XorDiscount extends LogicalDiscount {
    constructor(
        firstDiscount : Discount,
        secondDiscount : Discount) {
        super(firstDiscount,secondDiscount);
    }
    public toString = (store:Store) : string => {
        return `only ONE of the discounts is supplied, not both: ${this.firstDiscount}     OR     ${this.secondDiscount}`;
    }
  }
  
  export default XorDiscount