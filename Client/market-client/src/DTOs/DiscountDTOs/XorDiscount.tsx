import LogicalDiscount from "./LogicalDiscount";
import Discount from "./Discount";
import Store from "../Store";

class XorDiscount extends LogicalDiscount {
    constructor(
        firstExpression : Discount,
        secondExpression : Discount) {
        super(firstExpression,secondExpression,'XorDiscount');
    }
    public toString = (store:Store) : string => {
        return `only ONE of the discounts is supplied, not both: ${this.firstExpression.toString(store)}     OR     ${this.secondExpression.toString(store)}`;
    }
  }
  
  export default XorDiscount