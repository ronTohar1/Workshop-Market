import Predicate from "./Predicate";
import PurchasePolicy from "./PurchasePolicy";
import Restriction from "./Restriction";

class Implies extends PurchasePolicy {
    condition: Predicate;
    allowing: Predicate;
    constructor(
        condition: Predicate,
        allowing: Predicate) {
        super('ImpliesPurchase');
        this.condition = condition;
        this.allowing = allowing;
    }
    public toString = () : string => {
        return `IF ${this.condition.toString()} THEN NECESSERALLY ${this.allowing.toString()}`;
    }
  }
  
  export default Implies