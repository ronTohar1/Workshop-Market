import CheckProductLess from "./CheckProductLess";
import Predicate from "./Predicate";

class CheckProductMore extends Predicate {
    productId: number;
    productName:string | undefined;
    amount: number;
    constructor(
        productId: number,
        productName:string | undefined,
        amount: number) {
        super('CheckProductMorePredicate');
        this.productId = productId;
        this.productName=productName;
        this.amount = amount;
    }
    public toString = () : string => {
        return `The bag contains at least ${this.amount} ${this.productName}s`;
  }
}
  export default CheckProductMore