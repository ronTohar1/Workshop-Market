
import Restriction from "./Restriction";

class AfterHourProduct extends Restriction {
    productId: number;
    productName: string | undefined;
    amount: number;
    hour:number;
    constructor(
        hour: number,
        productName: string| undefined,
        productId: number,
        amount: number) {
        super('AfterHourAmountRestriction');
        this.productName=productName;
        this.hour = hour;
        this.productId = productId;
        this.amount = amount;
    }
    public toString = () : string => {
        return `The store stops selling ${this.productName} after: ${this.hour}:00 in quantities of ${this.amount} or more`;
    }
  }
  
  export default AfterHourProduct