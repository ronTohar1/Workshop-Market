import Restriction from "./Restriction";

class DateRestriction extends Restriction {
    year : number;
    month : number;
    day : number;
    constructor(
        year : number,
        month : number,
        day : number
    ) {
        super('DateRestriction');
        this.year = year;
        this.month = month;
        this.day = day;
    }
    public toString = () : string => {
        var today = new Date();
        if (this.year==-1){
            if (this.month==-1){
                if (this.day==-1){
                    return `the store does not provide services at: ${today.getDay()}/${today.getMonth()+1}/${today.getFullYear()}`;
                }
                else{
                    return `the store does not provide services at: ${this.day}/${today.getMonth()+1}/${today.getFullYear()}`;
                }
            }
            else{
                if (this.day==-1){
                    return `the store does not provide services at: ${today.getDay()}/${this.month}/${today.getFullYear()}`;
                }
                else{
                    return `the store does not provide services at: ${this.day}/${this.month}/${today.getFullYear()}`;
                }
            }
        }
        else{
            if (this.month==-1){
                if (this.day==-1){
                    return `the store does not provide services at: ${today.getDay()}/${today.getMonth()+1}/${this.year}`;
                }
                else{
                    return `the store does not provide services at: ${this.day}/${today.getMonth()+1}/${this.year}`;
                }
            }
            else{
                if (this.day==-1){
                    return `the store does not provide services at: ${today.getDay()}/${this.month}/${this.year}`;
                }
                else{
                    return `the store does not provide services at: ${this.day}/${this.month}/${this.year}`;
                }
            }
        }
  }
}
  export default DateRestriction