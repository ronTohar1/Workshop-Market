import Purchase from "./Purchase";
import Restriction from "./Restriction";

class Implies extends Purchase {
    condition: Restriction;
    allowing: Restriction;
    constructor(
        condition: Restriction,
        allowing: Restriction) {
        super();
        this.condition = condition;
        this.allowing = allowing;
    }
  }
  
  export default Implies