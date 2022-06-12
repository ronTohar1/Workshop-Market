import AfterHour from "./AfterHour";
import PurchasePolicy from "./PurchasePolicy";
import Restriction from "./Restriction";

class AndPolicy extends PurchasePolicy {
    firstPred: Restriction;
    secondPred: Restriction;
    constructor(
        firstPred: Restriction,
        secondPred: Restriction) {
        super('AndPurchase');
        this.firstPred = firstPred;
        this.secondPred = secondPred;
    }
    public toString = () : string => {
        return `${this.firstPred.toString()} AND ${this.secondPred.toString()}`;
    }
  }
  
  export default AndPolicy