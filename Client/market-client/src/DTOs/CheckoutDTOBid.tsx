import Bid from "./Bid";

class CheckoutDTOBid {
    bid:Bid | undefined;
    firstName:string;
    lastName:string;
    address: string;
    city: string;
    zip: string;
    country: string;

    nameOnCard: string;
    cardNumber: string;
    month: string;
    year: string;
    ccv: string;
    id: string;

  
    constructor() {
        this.bid=undefined;
        this.firstName = "";
        this.lastName = "";
        this.address = "";
        this.city = "";
        this.zip = "";
        this.country = "";
        this.city = "";

        this.nameOnCard = "";
        this.cardNumber = "";
        this.month = "";
        this.year = "";
        this.ccv = "";
        this.id = "";

    }
  }
  
  export default CheckoutDTOBid;
  
