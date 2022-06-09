import Store from "../Store";

class PurchasePolicy  {
    tag:string;
    constructor(tag:string) {
      this.tag=tag;
    }
    public toString = () : string => {
      return `Purchase`;
    }
  }
  
  export default PurchasePolicy