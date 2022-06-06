import AfterHour from "./AfterHour";
import Restriction from "./Restriction";

class AtLeastAmount extends Restriction {
    productId: number;
    amount: number;
    constructor(
        productId: number,
        amount: number) {
        super();
        this.productId = productId;
        this.amount = amount;
    }
  }
  
  export default AtLeastAmount