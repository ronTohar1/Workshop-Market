
class Purchase {
    purchaseDate: Date;
    purchasePrice: number;
    purchaseDescription: string;
  
    constructor(
        purchaseDate: Date,
        purchasePrice: number,
        purchaseDescription: string,
  
    ) {
      this.purchaseDate = purchaseDate;
      this.purchasePrice = purchasePrice;
      this.purchaseDescription = purchaseDescription;
    }
  }
  
  export default Purchase;
  
