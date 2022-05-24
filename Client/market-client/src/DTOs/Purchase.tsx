
class Purchase {
    purchaseDate: string;
    purchasePrice: number;
    purchaseDescription: string;
    buyerId: number;
  
    constructor(
        purchaseDate: string,
        purchasePrice: number,
        purchaseDescription: string,
        buyerId: number
  
    ) {
      this.purchaseDate = purchaseDate;
      this.purchasePrice = purchasePrice;
      this.purchaseDescription = purchaseDescription;
      this.buyerId = buyerId;
    }
  }
  
  export default Purchase;
  
