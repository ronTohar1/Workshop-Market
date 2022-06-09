import Predicate from "./Predicate";

class CheckProductLess extends Predicate {
    productId: number;
    productName:string | undefined;
    amount: number;
    constructor(
        productId: number,
        productName:string | undefined,
        amount: number) {
        super('CheckProductLessPredicate');
        this.productId = productId;
        this.productName=productName;
        this.amount = amount;
    }
    public toString = () : string => {
        return `The bag contains at most ${this.amount} ${this.productName}s`;
    }
  }
  
  export default CheckProductLess