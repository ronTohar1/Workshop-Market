import LogicalDiscount from "./LogicalDiscount";
import Discount from "./Discount";
import Store from "../Store";

class AndDiscount extends LogicalDiscount {
    constructor(
        firstExpression : Discount,
        secondExpression : Discount) {
        super(firstExpression,secondExpression,'AndDiscount');
    }
    public toString = (store:Store) : string => {
        return `${this.firstExpression.toString(store)}     AND     ${this.secondExpression.toString(store)}`;
    }
  }
  
  export default AndDiscount