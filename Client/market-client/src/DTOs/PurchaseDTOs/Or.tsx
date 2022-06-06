import Purchase from "./Purchase";
import Restriction from "./Restriction";

class Or extends Purchase {
    firstPred: Restriction;
    secondPred: Restriction;
    constructor(
        firstPred: Restriction,
        secondPred: Restriction) {
        super();
        this.firstPred = firstPred;
        this.secondPred = secondPred;
    }
  }
  
  export default Or