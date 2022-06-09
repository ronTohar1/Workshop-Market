import Store from "../Store";
import PurchasePolicy from "./PurchasePolicy";

class Restriction extends PurchasePolicy {
    constructor(tag:string) {
        super(tag);
    }
    public toString = () : string => {
        return `Purchase`;
      }
  }
  
  export default Restriction