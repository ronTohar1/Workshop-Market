import Store from "../Store";
import Discount from "./Discount";
import Expression from "./Expression";
import Predicate from "./Predicate";
import StoreDiscount from "./StoreDiscount";

class ProductDiscount extends Discount {
    pid : number;
    discount : number;
    constructor(
        pid : number,
        discount : number
        ) {
        super("productDiscount");
        this.pid = pid;
        this.discount = discount;
    }
    public toString = (store:Store) : string => {
        return `there is a ${this.discount}% discount on ${store.products.find(p=>p.id==this.pid)!=undefined?(store.products.find(p=>p.id==this.pid)?.name):''} !`;
    }
  }
  
  export default ProductDiscount
  