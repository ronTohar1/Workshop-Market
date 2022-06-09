import AfterHour from "./AfterHour";
import AtLeastAmount from "./AtLeastAmount";
import Restriction from "./Restriction";

class AtMostAmount extends Restriction {
    productId: number;
    productName: string | undefined;
    amount: number;
    constructor(
        productId: number,
        productName: string | undefined,
        amount: number) {
        super('AtMostAmountRestriction');
        this.productId = productId;
        this.productName=productName;
        this.amount = amount;
    }
    public toString = () : string => {
        return `The bag can contain at most ${this.amount} ${this.productName}s`;
    }
  }
  
  export default AtMostAmount