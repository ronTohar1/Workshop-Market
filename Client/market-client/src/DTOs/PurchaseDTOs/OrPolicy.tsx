import PurchasePolicy from "./PurchasePolicy";
import Restriction from "./Restriction";

class OrPolicy extends PurchasePolicy {
    firstPred: Restriction;
    secondPred: Restriction;
    constructor(
        firstPred: Restriction,
        secondPred: Restriction) {
        super('OrPurchase');
        this.firstPred = firstPred;
        this.secondPred = secondPred;
    }
    public toString = () : string => {
        return `${this.firstPred.toString()} OR ${this.secondPred.toString()}`;
    }
  }
  
  export default OrPolicy