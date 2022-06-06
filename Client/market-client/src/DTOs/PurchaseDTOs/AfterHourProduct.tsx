import AfterHour from "./AfterHour";

class AfterHourProduct extends AfterHour {
    productId: number;
    amount: number;
    constructor(
        hour: number,
        productId: number,
        amount: number) {
        super(hour);
        this.productId = productId;
        this.amount = amount;
    }
  }
  
  export default AfterHourProduct