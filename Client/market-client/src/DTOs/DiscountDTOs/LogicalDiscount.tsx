import Expression from "./Expression";
import Discount from "./Discount";
import Store from "../Store";

class LogicalDiscount extends Discount {
    firstExpression : Discount;
    secondExpression : Discount;
    constructor(
        firstExpression : Discount,
        secondExpression : Discount,
        tag:string) {
        super(tag);
        this.firstExpression = firstExpression;
        this.secondExpression = secondExpression;
    }
    public toString = (store:Store) : string => {
        return `${this.firstExpression} , ${this.secondExpression}`;
    }
  }
  
  export default LogicalDiscount