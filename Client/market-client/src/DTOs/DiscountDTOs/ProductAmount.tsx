import Store from "../Store";
import Expression from "./Expression";
import Predicate from "./Predicate";

class ProductAmount extends Predicate {
    pid : number;
    quantity : number;
    constructor(
        pid : number,
        quantity : number
        ) {
        super();
        this.pid = pid;
        this.quantity = quantity;
    }
    public toString = (store:Store) : string => {
        return `THE BAG CONTAINS AT LEAST ${this.quantity} ${store.products.find(p=>p.id==this.pid)?.name}s`;
    }
  }
  
  export default ProductAmount