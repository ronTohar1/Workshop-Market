import AfterHour from "./AfterHour";
import AtLeastAmount from "./AtLeastAmount";
import Restriction from "./Restriction";

class AtMostAmount extends AtLeastAmount {
    constructor(
        productId: number,
        amount: number) {
        super(productId,amount);
    }
  }
  
  export default AtMostAmount