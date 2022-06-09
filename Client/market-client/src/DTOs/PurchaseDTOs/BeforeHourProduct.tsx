import Restriction from "./Restriction";

class BeforeHourProduct extends Restriction {
    productId: number;
    productName: string | undefined;
    amount: number;
    hour:number;
    constructor(
        hour: number,
        productName: string | undefined,
        productId: number,
        amount: number) {
        super('BeforeHourProductRestriction');
        this.productName=productName;
        this.hour = hour;
        this.productId = productId;
        this.amount = amount;
    }
    public toString = () : string => {
        return `The store begins selling ${this.productName} in quantities of ${this.amount} only from: ${this.hour}:00`;
    }
  }
  
  export default BeforeHourProduct