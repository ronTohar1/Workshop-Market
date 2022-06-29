import AfterHour from "./AfterHour";
import Restriction from "./Restriction";

class AtLeastAmount extends Restriction {
    productId: number;
    productName: string | undefined;
    amount: number;
    constructor(
        productId: number,
        productName: string | undefined,
        amount: number) {
        super('AtLeastAmountRestriction');
        this.productId = productId;
        this.productName=productName;
        this.amount = amount;
    }
    public toString = () : string => {
        return `The bag needs to contain at least ${this.amount} ${this.productName}s`;
    }
  }
  
  export default AtLeastAmount