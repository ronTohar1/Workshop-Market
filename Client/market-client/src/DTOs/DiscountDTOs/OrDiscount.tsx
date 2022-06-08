import LogicalDiscount from "./LogicalDiscount";
import Discount from "./Discount";
import Store from "../Store";

class OrDiscount extends LogicalDiscount {
    constructor(
        firstExpression : Discount,
        secondExpression : Discount) {
        super(firstExpression,secondExpression, 'OrDiscount');
    }
    public toString = (store:Store) : string => {
        return `${this.firstExpression.toString(store)}     OR     ${this.secondExpression.toString(store)}`;
    }
  }
  
  export default OrDiscount