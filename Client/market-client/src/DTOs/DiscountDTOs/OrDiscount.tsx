import LogicalDiscount from "./LogicalDiscount";
import Discount from "./Discount";
import Store from "../Store";

class OrDiscount extends LogicalDiscount {
    constructor(
        firstDiscount : Discount,
        secondDiscount : Discount) {
        super(firstDiscount,secondDiscount);
    }
    public toString = (store:Store) : string => {
        return `${this.firstDiscount}     OR     ${this.secondDiscount}`;
    }
  }
  
  export default OrDiscount