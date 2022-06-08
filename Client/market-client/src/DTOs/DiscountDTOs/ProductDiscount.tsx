import Store from "../Store";
import Expression from "./Expression";
import Predicate from "./Predicate";
import StoreDiscount from "./StoreDiscount";

class ProductDiscount extends StoreDiscount {
    pid : number;
    constructor(
        pid : number,
        discount : number
        ) {
        super(discount);
        this.pid = pid;
    }
    public toString = (store:Store) : string => {
        return `there is a ${this.discount}% discount on ${store.products.find(p=>p.id==this.pid)?.name} !`;
    }
  }
  
  export default ProductDiscount
  