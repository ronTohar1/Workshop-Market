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
    public toString = () : string => {
        if (this.year==-1){
            if (this.month==-1){
                if (this.day==-1){
                    return `there isn't a date discount`;
                }
                else{
                    return `each ${this.day}th day there is a ${this.discount}% discount at the store!`;
                }
            }
            else{
                if (this.day==-1){
                    return `each ${this.month}th month there is a ${this.discount}% discount at the store!`;
                }
                else{
                    return `once a year, at the ${this.day}/${this.month} there is a ${this.discount}% discount at the store!`;
                }
            }
        }
        else{
            if (this.month==-1){
                if (this.day==-1){
                    return `at ${this.year} there is a ${this.discount}% discount at the store!`;
                }
                else{
                    return `at ${this.year}, at each month at the ${this.day} there is a ${this.discount}% discount at the store!`;
                }
            }
            else{
                if (this.day==-1){
                    return `at the ${this.month}/${this.year} there is a ${this.discount}% discount at the store!`;
                }
                else{
                    return `at the ${this.day}/${this.month}/${this.year} there is a ${this.discount}% discount !`;
                }
            }
        }
    }
  }
  
  export default DateDiscount