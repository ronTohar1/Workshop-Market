import StoreDiscount from "./StoreDiscount";

class DateDiscount extends StoreDiscount {
    year : number;
    month : number;
    day : number;
    constructor(
        discount : number,
        year : number,
        month : number,
        day : number) {
        super(discount);
        this.year = year;
        this.month = month;
        this.day = day;
    }
  }
  
  export default DateDiscount