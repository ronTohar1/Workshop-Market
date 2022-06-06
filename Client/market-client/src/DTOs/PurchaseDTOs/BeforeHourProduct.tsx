import BeforeHour from "./BeforeHour";

class BeforeHourProduct extends BeforeHour {
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
  
  export default BeforeHourProduct