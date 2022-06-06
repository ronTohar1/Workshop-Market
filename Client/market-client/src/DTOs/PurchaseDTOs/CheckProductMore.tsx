import CheckProductLess from "./CheckProductLess";

class CheckProductMore extends CheckProductLess {
    constructor(
        productId: number,
        amount: number) {
        super(productId, amount);
    }
  }
  
  export default CheckProductMore