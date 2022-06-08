import LogicalDiscount from "./LogicalDiscount";
import Discount from "./Discount";
import Store from "../Store";

class AndDiscount extends LogicalDiscount {
    constructor(
        firstDiscount : Discount,
        secondDiscount : Discount) {
        super(firstDiscount,secondDiscount);
    }
    public toString = (store:Store) : string => {
        return `${this.firstDiscount}     AND     ${this.secondDiscount}`;
    }
  }
  
  export default AndDiscount